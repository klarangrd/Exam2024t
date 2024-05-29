using Microsoft.AspNetCore.Mvc;
using Serverapi.Repositories;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Cors;
using GemBox.Pdf;
using GemBox.Pdf.Content;
using System.IO;
using System;
using System.Drawing; //for signature image
using System.Drawing.Imaging;//for signature image
using System.Text;

namespace Serverapi.Controllers
{
    [ApiController]
    [Route("api/application")]
    [EnableCors("AllowSpecificOrigin")]
    public class ApplicationController : ControllerBase
    {
        private readonly Iapplicationrepository _appRepository;

        public ApplicationController(Iapplicationrepository applicationRepository)
        {
            _appRepository = applicationRepository;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllApplications()
        {
            var applications = await _appRepository.GetAllApplications();
            return Ok(applications);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddApplication([FromBody] Application application)
        {
            if (application == null)
            {
                return BadRequest("Application cannot be null.");
            }

            await _appRepository.Add(application);
            return CreatedAtAction(nameof(GetAllApplications), new { id = application.Id }, application);
        }

        [HttpPut]
        [Route("update/{appId}")]
        public async Task<IActionResult> UpdateApplication(int appId, [FromBody] Application application)
        {
            if (application == null)
            {
                return BadRequest("Application cannot be null.");
            }

            try
            {
                await _appRepository.UpdateApplication(appId, application);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("approved")]
        public async Task<IActionResult> GetApprovedApplications()
        {
            var applications = await _appRepository.GetApprovedApplications();
            return Ok(applications);
        }

        [HttpGet("queued")]
        public async Task<IActionResult> GetQueuedApplications()
        {
            var applications = await _appRepository.GetQueuedApplications();
            return Ok(applications);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public void DeleteApplication(int id)
        {
            _appRepository.DeleteApplication(id);
        }

        [HttpGet("newsletter")]
        public async Task<IActionResult> GetVolunteerEmails()
        {
            var emails = await _appRepository.GetVolunteerEmails();
            return Ok(emails);
        }

        //to download the application as a pdf
        [HttpGet("downloadpdf/{appId}")]

        //handle the PDF download request, the appid in the url will be mapped to the appId parameter
        public async Task<IActionResult> DownloadPdf(int appId)
        {
            try
            {
                //logging start pdf generation
                Console.WriteLine($"Starting PDF generation for application ID {appId}");

                //fetching application details by id, using the application repository
                var application = await _appRepository.GetApplicationById(appId);
                if (application == null)
                {
                    Console.WriteLine($"Application not found for ID {appId}");
                    return NotFound("Application not found."); //if not fetched return error
                }

                Console.WriteLine($"Application found: {application.Child.Volunteer.Name}");

                //setting the license key for the nuget gembox.pdf
                ComponentInfo.SetLicense("FREE-LIMITED-KEY");

                byte[] pdfBytes;

                //creating a memory stream (storing in memory) to store the pdf document
                using (var ms = new MemoryStream())
                {
                    using (var document = new PdfDocument())
                    {
                        //new page for the pdf file
                        var page = document.Pages.Add();

                        //starting position for the text
                        var yPosition = 700;

                        //define a title style
                        var titleText = new PdfFormattedText();
                        titleText.Font = new PdfFont("Arial", 20);
                        titleText.AppendLine("Ansøgningsdetaljer");
                        page.Content.DrawText(titleText, new PdfPoint(50, yPosition));

                        //adjust yPosition for the next section
                        yPosition -= 50;

                        //define a section header style
                        var headerText = new PdfFormattedText();
                        headerText.Font = new PdfFont("Arial", 14);

                        //define a normal text style
                        var bodyText = new PdfFormattedText();
                        bodyText.Font = new PdfFont("Arial", 12);

                        //application details
                        headerText.Clear();
                        headerText.AppendLine("Frivillig Information");
                        page.Content.DrawText(headerText, new PdfPoint(50, yPosition));
                        yPosition -= 50;

                        bodyText.Clear();
                        bodyText.AppendLine($"Kræwnr: {application.Child.Volunteer.Kræwnr}");
                        bodyText.AppendLine($"Frivillig Navn: {application.Child.Volunteer.Name}");
                        bodyText.AppendLine($"Email: {application.Child.Volunteer.Email}");
                        page.Content.DrawText(bodyText, new PdfPoint(50, yPosition));
                        yPosition -= 50;

                        headerText.Clear();
                        headerText.AppendLine("Barn Information");
                        page.Content.DrawText(headerText, new PdfPoint(50, yPosition));
                        yPosition -= 90;

                        bodyText.Clear();
                        bodyText.AppendLine($"Barn Navn: {application.Child.ChildName}");
                        bodyText.AppendLine($"Barn Alder: {application.Child.ChildAge}");
                        bodyText.AppendLine($"Tøjsstørrelse: {application.Child.ClothingSize}");
                        bodyText.AppendLine($"Kommentar: {application.Child.Comment}");
                        bodyText.AppendLine($"Været her før?: {application.Child.Beenbefore}");
                        bodyText.AppendLine($"Interesse: {application.Child.Interest}");
                        page.Content.DrawText(bodyText, new PdfPoint(50, yPosition));
                        yPosition -= 50;

                        headerText.Clear();
                        headerText.AppendLine("Ansøgningsdetaljer");
                        page.Content.DrawText(headerText, new PdfPoint(50, yPosition));
                        yPosition -= 90;

                        bodyText.Clear();
                        bodyText.AppendLine($"Frivillig?: {application.IsVolunteer}");
                        bodyText.AppendLine($"Første Prioritets Uge: {application.FirstpriorityWeek}");
                        bodyText.AppendLine($"Første Prioritets Periode: {application.FirstpriorityPeriod}");
                        bodyText.AppendLine($"Anden Prioritets Uge: {application.SecondpriorityWeek}");
                        bodyText.AppendLine($"Anden Prioritets Periode: {application.SecondpriorityPeriod}");
                        bodyText.AppendLine($"Er ansøgningen underskrevet?: {application.Signature.Signed}");
                        page.Content.DrawText(bodyText, new PdfPoint(50, yPosition));
                        yPosition -= 40;

                        //checking if the application is signed and has a signature image
                        if (application.Signature?.Signed == true && application.Signature?.Sign != null)
                        {
                            try
                            {
                                Console.WriteLine($"Signature byte array length: {application.Signature.Sign.Length}");
                                Console.WriteLine($"Signature byte array content: {BitConverter.ToString(application.Signature.Sign.Take(20).ToArray())}");

                                //convert the byte array to a string
                                var base64String = Encoding.UTF8.GetString(application.Signature.Sign);

                                //remove the data:image/png;base64, part if present
                                var base64Data = base64String.Split(',')[1];
                                Console.WriteLine($"Base64 data: {base64Data.Substring(0, 50)}...");

                                //decode the base64 string to get the raw image bytes
                                var imageBytes = Convert.FromBase64String(base64Data);

                                using (var signatureImageStream = new MemoryStream(imageBytes))
                                {
                                    //validate if the byte array is a valid image
                                    var signatureImage = Image.FromStream(signatureImageStream, useEmbeddedColorManagement: true, validateImageData: true);

                                    //save the image as a temporary file in png
                                    using (var tempImageStream = new MemoryStream())
                                    {
                                        signatureImage.Save(tempImageStream, ImageFormat.Png);
                                        tempImageStream.Position = 0; //reset the stream position

                                        var signaturePdfImage = PdfImage.Load(tempImageStream);
                                        var position = new PdfPoint(50, yPosition - 100);
                                        var size = new PdfSize(signatureImage.Width * 0.50, signatureImage.Height * 0.50); //image size
                                        page.Content.DrawImage(signaturePdfImage, position, size);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error processing signature image: {ex.Message}");
                                Console.WriteLine(ex.StackTrace);
                                page.Content.DrawText(new PdfFormattedText().AppendLine("Signature image is invalid"), new PdfPoint(50, yPosition - 100));
                            }
                        }

                        //saving the pdf to the memory stream
                        document.Save(ms);
                    }

                    //memory stream to byte array
                    pdfBytes = ms.ToArray();
                }

                Console.WriteLine($"PDF generation successful for application ID {appId}");

                return File(pdfBytes, "application/pdf", $"application_{appId}.pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating PDF for application ID {appId}: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return StatusCode(500, "Internal server error. Please check the server logs for more details.");
            }
        }


    }
}
