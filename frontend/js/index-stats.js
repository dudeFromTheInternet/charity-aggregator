document.addEventListener("DOMContentLoaded", () => {
  fetch(`http://localhost:80/CharityProjects/statistic`, {
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
