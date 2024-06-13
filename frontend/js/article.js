const id = localStorage.getItem("requestedArticleId");

window.addEventListener("DOMContentLoaded", function() {
  fetch(`http://localhost:80/Article/${id}/`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    }
  })
    .then(response => response.json())
    .then(data => displayArticleData(data))
    .catch(error => console.error('Error:', error));
});

window.addEventListener("beforeunload", function() {
  localStorage.setItem("requestedArticleId", id);
});



function displayArticleData(data) {
  document.querySelector('.article-title').textContent = data.title;

  document.querySelector('.article').textContent = data.text;

}
