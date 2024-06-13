document.addEventListener("DOMContentLoaded", () => {
  if (getCookie("isLoggedIn") === "true"){
    window.location.assign("index.html");
  }
  const loginButton = document.getElementById("login");
  const userName = document.getElementById("userName");
  const password = document.getElementById("password");
  loginButton.addEventListener("click", () => {
    if (userName.value !== "" && password.value !== "") {
      document.cookie = encodeURIComponent("isLoggedIn") + "=" + encodeURIComponent(true);
    }
  })
});
function getCookie(name) {
  let matches = document.cookie.match(new RegExp(
    "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
  ));
  return matches ? decodeURIComponent(matches[1]) : undefined;
}
