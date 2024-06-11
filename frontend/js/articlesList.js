document.addEventListener('DOMContentLoaded', function() {
  fetch('http://localhost:80/Article/', {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    }
  })
    .then(response => response.json())
    .then(data => updateArticlesDisplay(data))
    .catch(error => console.error('Error:', error));
  });
document.getElementById('submit-button').addEventListener('click', function() {
  const name = document.querySelector('#name-filter').value;
  const startDateRaw = document.querySelector('#start-date').value;
  const endDateRaw = document.querySelector('#end-date').value;
  const startDate = startDateRaw ? new Date(startDateRaw).toISOString() : '';
  const endDate = endDateRaw ? new Date(endDateRaw).toISOString() : '';
  const charityName = document.querySelector('#charity-filter').value;
  const queryParams = new URLSearchParams({
    title: name,
    startFilterDate: startDate,
    endFilterDate: endDate,
    author: charityName,
  }).toString();

  fetch(`http://localhost:80/Article/filter?${queryParams}`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    }
  })
    .then(response => response.json())
    .then(data => updateArticlesDisplay(data, true))
    .catch(error => console.error('Error:', error));
});

function updateArticlesDisplay(articles, clear) {
  const articlesContainer = document.querySelector('.articles-container');
  if (clear){
    while (articlesContainer.firstChild) {
      articlesContainer.removeChild(articlesContainer.firstChild);
    }
  }
  articles.forEach(article => {
    const publicationDate = new Date(article.publicationDate).toLocaleDateString();
    const li = document.createElement('li');
    const projectLink = document.createElement('a');
    projectLink.className = 'article';
    projectLink.id = article.id;
    projectLink.href = 'article.html';
    projectLink.addEventListener('click', (e) => {
      localStorage.setItem('requestedArticleId', article.articleId);
    })

    const title = document.createElement('h2');
    title.className = 'article-list-title';
    title.textContent = article.title;

    const charityName = document.createElement('h5');
    charityName.textContent = article.author;
    const projectTime = document.createElement('h5');
    projectTime.textContent = new Date(article.publicationDate).toLocaleDateString();

    const projectDescription = document.createElement('pre');
    projectDescription.className = 'article-list-summary';
    projectDescription.textContent = article.text;

    const projectPhoto = document.createElement('img');
    projectPhoto.src = '../img/projectPlaceholder.png';
    projectPhoto.alt = 'article photo';

    projectLink.appendChild(projectPhoto);
    projectLink.appendChild(title);
    projectLink.appendChild(projectDescription);
    projectLink.appendChild(charityName);
    projectLink.appendChild(projectTime);
    li.appendChild(projectLink)
    articlesContainer.appendChild(li);
  });
}



