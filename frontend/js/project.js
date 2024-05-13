const id = localStorage.getItem("requestedProjectId");

window.addEventListener("DOMContentLoaded", function() {
  fetch(`http://158.160.82.113:80/CharityProjects/${id}/`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    }
  })
    .then(response => response.json())
    .then(data => displayProjectData(data))
    .catch(error => console.error('Error:', error));
});

window.addEventListener("beforeunload", function() {
  localStorage.setItem("requestedProjectId", id);
});



function displayProjectData(data) {
  document.querySelector('.project-title').textContent = data.name;

  document.querySelector('.project-category').textContent = data.category;

  const dateSpan = document.querySelector('.project-flex-ver span');
  dateSpan.textContent = `${new Date(data.startDate).toLocaleDateString()} - ${new Date(data.endDate).toLocaleDateString()}`;

  document.querySelector('.project-charity').textContent = data.charityName;

  document.querySelector('.project-flex-ver.project-main-info p').textContent = data.description;

  const projectImage = document.querySelector('.project-flex-hor img');
  projectImage.alt = `Logo of ${data.name}`;

  document.querySelector('.project-contacts').textContent = data.contactInfo;
}
const modal = document.getElementById('contactModal');
const openButton = document.querySelector('.openModal');
const closeButton = document.querySelector('.close');
openButton.addEventListener('click', function() {
  modal.style.display = "block";
});

closeButton.addEventListener('click', function() {
  document.getElementById('contactModal').style.display = "none";
});
