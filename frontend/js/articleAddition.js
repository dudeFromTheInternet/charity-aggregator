const additionButton = document.getElementById('submit-addition');
const additionText = document.getElementById('addition-completion');

additionButton.addEventListener('click', function () {
  additionText.innerText = "";
  const name = document.getElementById('addition-name').value;
  const text = document.getElementById('addition-text').value;
  const charityName = 'Example Charity';
  const photo = document.getElementById('addition-image').files[0];

  const formData = {
    title: name,
    text: text,
    publicationDate: new Date().toISOString(),
    author: charityName,
    photo: photo ? URL.createObjectURL(photo) : '../img/icon.png'
  };
  fetch('http://localhost:80/Article', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(formData)
  })
    .then(response => additionText.innerText = "Добавлено!")
    .catch(error => {
      console.error('Error:', error);
      additionText.innerText = "Ошибка, попробуйте еще раз позже"
    });
})
