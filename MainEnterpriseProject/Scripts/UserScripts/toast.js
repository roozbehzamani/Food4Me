function launch_toast() {
    var x = document.getElementById("toast")
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 5000);
}


function myFunction() {
    document.getElementById("myForm").reset();
}

window.onload = myFunction;
window.onload = launch_toast;