const data = [
    {
        name: "Проект 1",
        category: "Категория 1",
        description: "Описание проекта 1",
        photoUrl: "../img/project1.png",
        projectUrl: "project1.html"
    },
    {
        name: "Проект 2",
        category: "Категория 2",
        description: "Описание проекта 2",
        photoUrl: "../img/project2.png",
        projectUrl: "project2.html"
    }
];

const projectsContainer = document.querySelector('.projects-container');

data.forEach(project => {
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

    const projectDescription = document.createElement('p');
    projectDescription.className = 'project-description';
    projectDescription.textContent = project.description;

    const projectPhoto = document.createElement('img');
    projectPhoto.className = 'project-photo';
    projectPhoto.src = project.photoUrl;
    projectPhoto.alt = 'project photo';

    projectInfo.appendChild(projectName);
    projectInfo.appendChild(projectCategory);
    projectInfo.appendChild(projectDescription);
    projectLink.appendChild(projectInfo);
    projectLink.appendChild(projectPhoto);

    projectsContainer.appendChild(projectLink);
});

function submit() {
    console.log('submit');
}