const additionButton = document.getElementById('submit-addition');
const additionText = document.getElementById('addition-completion');
additionButton.addEventListener('click', function () {
  additionText.innerText = "";
  fetch('https://localhost:7158/CharityProjects', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    }
  })
    .then(response => additionText.innerText = "Добавлено!")
    .catch(error => {
      console.error('Error:', error);
      additionText.innerText = "Ошибка, попробуйте еще раз позже"
    });
})
