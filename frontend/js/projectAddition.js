const additionButton = document.getElementById('submit-addition');
const additionText = document.getElementById('addition-completion');

additionButton.addEventListener('click', function () {
  additionText.innerText = "";
  const name = document.getElementById('addition-name').value;
  const description = document.getElementById('addition-text').value;
  const startDateRaw = document.getElementById('addition-start-date').value;
  const endDateRaw = document.getElementById('addition-end-date').value;
  const startDate = startDateRaw ? new Date(startDateRaw).toISOString() : new Date().toISOString();
  const endDate = endDateRaw ? new Date(endDateRaw).toISOString() : new Date("2100-12-12").toISOString();
  const categories = document.getElementById('addition-category')
    .value
    .split(',')
    .map(category => category.trim());
  const charityName = 'Example Charity';
  const photo = document.getElementById('addition-image').files[0];
  const phone_number = document.getElementById('addition-phone').value;
  const credit_number = document.getElementById('addition-credit').value;
  const ref = document.getElementById('addition-ref').value;
  const formData = {
    name: name,
    description: description,
    startDate: startDate,
    endDate: endDate,
    category: categories,
    charityName: charityName,
    photo: photo ? URL.createObjectURL(photo) : '../img/icon.png',
    reference: ref,
    phoneNumber: phone_number,
    creditNumber: credit_number,
  };
  fetch('http://158.160.82.113:80/CharityProjects/', {
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
