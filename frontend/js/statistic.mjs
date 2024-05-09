document.addEventListener("DOMContentLoaded", () => {
  fetch(`https://localhost:7158/CharityProjects/statistic`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    }
  })
    .then(response => response.json())
    .then(data => displayStats(data))
    .catch(error => console.error('Error:', error));

})



function displayStats(data){
  const h2 = document.querySelector('.stats-text');
  h2.textContent = `На нашем сайте вы найдете ${data.projectCount} ${getCorrectFormProjects(data.projectCount)}
   среди ${data.categoriesCount} категор${getCorrectFormII(data.categoriesCount)}
   от ${data.charityCount} организац${getCorrectFormII(data.charityCount)} и ${getCorrectFormFonds(data.charityCount)}`;
  const canvas1 = document.getElementById('histogram');
  const ctx1 = canvas1.getContext('2d');


  const dates = [];
  for (let i = 0; i < data.visitsWeekly.length; i++) {
    const date = new Date();
    date.setDate(date.getDate() - i);
    dates.push(date.toLocaleDateString());
  }
  dates.reverse();
  new Chart(ctx1, {
    type: 'bar',
    data: {
      labels: dates,
      datasets: [{
        label: 'Число посетителей сайта',
        data: data.visitsWeekly,
        borderWidth: 1,
        backgroundColor: '#90C6F9FF',
        borderColor: '#0c4970',
      }]
    },
    options: {
      scales: {
        y: {
          beginAtZero: true
        }
      }
    }
  });
  const canvas2 = document.getElementById('histogram2');
  const ctx2 = canvas2.getContext('2d');



  dates.reverse();
  const helpedChart = new Chart(ctx2, {
    type: 'bar',
    data: {
      labels: dates,
      datasets: [{
        label: 'Число помогших людей',
        data: data.helpedWeekly,
        borderWidth: 1,
        backgroundColor: '#F4E0D2FF',
        borderColor: '#675d59',
      }]
    },
    options: {
      scales: {
        y: {
          beginAtZero: true,

        }
      }
    }
  });
}

function getCorrectFormII(count){
  if (count === 1) {
    return 'ии';
  }
  return 'ий';
}
function getCorrectFormProjects(count){
  const singleDigitRemainder = count % 10;
  const twoDigitRemainder = count % 100;

  if ((singleDigitRemainder === 0 || singleDigitRemainder >= 5)
    || (twoDigitRemainder >= 11 && twoDigitRemainder < 20)){
    return "проектов";
  }
  if (singleDigitRemainder === 1){
    return "проект";
  }
  return "проекта";
}

function getCorrectFormFonds(count){
  if (count === 1) {
    return 'фонда';
  }
  return 'фондов';
}

