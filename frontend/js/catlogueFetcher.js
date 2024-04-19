const mockData = [
    {
        name: "Проект 1",
        category: "Категория 1",
        description: "Описание проекта 1",
        charityName: "Организация 1",
        photoUrl: "../img/project1.png",
        projectUrl: "project1.html",
        startDate: "2024-03-01",
        endDate: "2024-06-01",
    },
    {
        name: "Проект 2",
        category: "Категория 2",
        description: "Описание проекта 2",
        charityName: "Организация 2",
        photoUrl: "../img/project2.png",
        projectUrl: "project2.html",
        startDate: "01.03.2024",
        endDate: "01.04.2024",
    }
];


document.addEventListener('DOMContentLoaded', function() {
    updateProjectsDisplay(mockData);
    /*
    fetch('your-backend-url', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
        })
    })
        .then(response => response.json())
        .then(data => updateProjectsDisplay(data))
        .catch(error => console.error('Error:', error));
     */
    });
document.getElementById('submit-button').addEventListener('click', function() {
    const name = document.querySelector('#name-filter').value;
    const startDate = document.querySelector('#start-date').value;
    const category = document.querySelector('#category-filter').value;
    const endDate = document.querySelector('#end-date').value;
    const charityName = document.querySelector('#charity-filter').value;
    fetch('your-backend-url', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            name: name,
            startDate: startDate,
            endDate: endDate,
            category: category,
            charityName: charityName
        })
    })
        .then(response => response.json())
        .then(data => updateProjectsDisplay(data, true))
        .catch(error => console.error('Error:', error));
    alert(JSON.stringify({
        name: name,
        startDate: startDate,
        endDate: endDate,
        category: category,
        charityName: charityName
    }));
});

function updateProjectsDisplay(projects, clear) {
    const projectsContainer = document.querySelector('.projects-container');
    if (clear){
        while (projectsContainer.firstChild) {
            projectsContainer.removeChild(projectsContainer.firstChild);
        }
    }
    projects.forEach(project => {
        const startDate = new Date(project.startDate).toLocaleDateString();
        const endDate = new Date(project.endDate).toLocaleDateString();
        const projectLink = document.createElement('a');
        projectLink.className = 'project';
        projectLink.href = project.projectUrl;

        const projectInfo = document.createElement('div');
        projectInfo.className = 'project-info';

        const projectName = document.createElement('h2');
        projectName.className = 'project-name';
        projectName.textContent = project.name;

        const projectCategory = document.createElement('h5');
        projectCategory.className = 'project-category';
        projectCategory.textContent = project.category;
        const charityName = document.createElement('h5');
        charityName.textContent = project.charityName;
        const projectTime = document.createElement('h5');
        projectTime.textContent = `${startDate} - ${endDate}`;

        const projectDescription = document.createElement('p');
        projectDescription.className = 'project-description';
        projectDescription.textContent = project.description;

        const projectPhoto = document.createElement('img');
        projectPhoto.className = 'project-photo';
        projectPhoto.src = project.photoUrl;
        projectPhoto.alt = 'project photo';

        projectInfo.appendChild(projectName);
        projectInfo.appendChild(projectCategory);
        projectInfo.appendChild(charityName);
        projectInfo.appendChild(projectTime);
        projectInfo.appendChild(projectDescription);
        projectLink.appendChild(projectInfo);
        projectLink.appendChild(projectPhoto);

        projectsContainer.appendChild(projectLink);
    });
}