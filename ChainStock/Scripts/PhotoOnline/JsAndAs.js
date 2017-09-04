function SetPhotoFrame(frameURL)
{
    callExternalInterface(frameURL);
}

function callExternalInterface(PicURL)
{
    thisMovie("My_Cam").SetFrame(PicURL);
}
//浏览器兼容访问DOM
function thisMovie(movieName)
{
    if (navigator.appName.indexOf("Microsoft") != -1)
    {
        return document.getElementById(movieName);
    }
    else
    {
        return document.getElementsByName(movieName)[0];
    }
}