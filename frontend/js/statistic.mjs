document.addEventListener("DOMContentLoaded", () => {
  fetch(`http://127.0.0.1:80/CharityProjects/statistic/`, {
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
  h2.innerHTML = `Количество проектов - ${data.projectCount}<br>
   Количество категорий проектов -  ${data.categoriesCount}<br>
   Количество благотворительных фондов и организаций - ${data.charityCount}`;
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


