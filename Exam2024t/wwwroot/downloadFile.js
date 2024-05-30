﻿console.log("downloadFile.js is loadedddddddddddddddddddddddddddddddddd");


window.downloadFile = function (url) {
    console.log("downloadFile function called with URL:", url);
    fetch(url)
        .then(response => response.blob())
        .then(blob => {
            const url = window.URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.style.display = 'none';
            a.href = url;
            a.download = 'application.pdf';  //file name
            document.body.appendChild(a);
            a.click();
            window.URL.revokeObjectURL(url);
        })
        .catch(() => alert('Failed to download file.'));
};
