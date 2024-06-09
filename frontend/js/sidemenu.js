document.addEventListener('DOMContentLoaded', function() {
  const menu = document.querySelector('.sidemenu');
  const toggleButton = document.querySelector('.sidemenu-button');


  toggleButton.onclick = function() {
    menu.classList.toggle('active');
    if(toggleButton.classList.toggle('active')){
      toggleButton.value = '<';
    } else {
      toggleButton.value = '>';
    }
  };
});
