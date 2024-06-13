document.addEventListener('DOMContentLoaded', function() {
    let header = document.querySelector('.header-main-dynamic');
    window.addEventListener('mousemove', function(e) {
        if (window.visualViewport.width >= 800 && e.clientY < 100 && window.scrollY  > 100) {
            header.classList.add('active');
        } else {
            header.classList.remove('active');
        }
    });
    window.addEventListener('scroll', function(e) {
        if (window.visualViewport.width >= 800 && window.scrollY < 100) {
            header.classList.remove('active');
        }
    });
    if (getCookie("isLoggedIn") === "true"){
      const navmenus = document.querySelectorAll('.navmenu');
      const authmenus = document.querySelectorAll('.authmenu');
      navmenus.forEach(element => {
        const li1 = document.createElement('li');
        const li2 = document.createElement('li');
        const a1 = document.createElement('a');
        const a2 = document.createElement('a');
        a1.href = "projectAddition.html";
        a1.textContent = "Добавить проект";
        a2.href = "articleAddition.html";
        a2.textContent = "Добавить статью";
        a1.className = 'menu button';
        a2.className = 'menu button';
        li1.appendChild(a1);
        li2.appendChild(a2);
        element.appendChild(li1);
        element.appendChild(li2);
      });
      authmenus.forEach(element => {
        while (element.firstChild) {
          element.removeChild(element.firstChild);
        }
        const li = document.createElement('li');
        const a = document.createElement('a');
        a.className = 'auth logIn button';
        a.href = "index.html";
        a.textContent = "Выйти";
        li.appendChild(a);
        element.appendChild(li);
        a.addEventListener('click', (e) => {
          document.cookie = encodeURIComponent("isLoggedIn") + "=" + encodeURIComponent(false);
        })
      })
    }
});

function getCookie(name) {
  let matches = document.cookie.match(new RegExp(
    "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
  ));
  return matches ? decodeURIComponent(matches[1]) : undefined;
}
