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
    fetch(`https://localhost:7158/CharityProjects/allCategories`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      }
    })
      .then(response => response.json())
      .then(data => updateCategoryFilter(data))
      .catch(error => console.error('Error:', error));
    fetch('https://localhost:7158/CharityProjects', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }
    })
      .then(response => response.json())
        .then(data => updateProjectsDisplay(data))
        .catch(error => console.error('Error:', error));
    const catSearch = document.getElementById('category-filter-search');
    catSearch.addEventListener('input', (event) => {
      const Checkboxes = document.querySelectorAll('input[name="category"]');
      Checkboxes.forEach(checkbox => {
        if (!checkbox.value.includes(catSearch.value) && !checkbox.checked) {
          checkbox.parentElement.parentElement.style.display = 'none';
          checkbox.style.display = 'none';
        } else {
          checkbox.parentElement.parentElement.style.display = '';
          checkbox.style.display = '';
        }
      });
    });
    const inputField = document.getElementById('category-filter-search');
    const checkboxGroup = document.querySelector('.checkbox-group');
    const container = document.querySelector('.category-search-container');

    inputField.addEventListener('click', function(event) {
      checkboxGroup.classList.toggle('hidden');
      event.stopPropagation();
    });

    document.addEventListener('click', function(event) {
      if (!container.contains(event.target)) {
        checkboxGroup.classList.add('hidden');
      }
    });
});
document.getElementById('submit-button').addEventListener('click', function() {
    const name = document.querySelector('#name-filter').value;
    const startDateRaw = document.querySelector('#start-date').value;
    const checkedCheckboxes = document.querySelectorAll('input[name="category"]:checked');
    const category = Array.from(checkedCheckboxes).map(checkbox => checkbox.value);
    const endDateRaw = document.querySelector('#end-date').value;
    const startDate = startDateRaw ? new Date(startDateRaw).toISOString() : '';
    const endDate = endDateRaw ? new Date(endDateRaw).toISOString() : '';
    const charityName = document.querySelector('#charity-filter').value;
    const queryParams = new URLSearchParams({
      name: name,
      startDate: startDate,
      endDate: endDate,
      charityName: charityName,
      category: category,
    }).toString();

    fetch(`https://localhost:7158/CharityProjects/filter?${queryParams}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      }
    })
      .then(response => response.json())
      .then(data => updateProjectsDisplay(data, true))
      .catch(error => console.error('Error:', error));
  });

function updateCategoryFilter(data) {
  data = data.sort((a, b) => a.localeCompare(b));
  const ulElement = document.querySelector('.checkbox-group');

  data.forEach(category => {
    const liElement = document.createElement('li');
    const labelElement = document.createElement('label');
    const inputElement = document.createElement('input');
    inputElement.type = 'checkbox';
    inputElement.name = 'category';
    inputElement.value = category;
    labelElement.appendChild(inputElement);
    labelElement.append(` ${category}`);
    liElement.appendChild(labelElement);
    ulElement.appendChild(liElement);
  });
}

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
        const li = document.createElement('li');
        const projectLink = document.createElement('a');
        projectLink.className = 'project';
        projectLink.id = project.id;
        projectLink.href = 'project.html';
        projectLink.addEventListener('click', (e) => {
          localStorage.setItem('requestedProjectId', project.id);
        })

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
        projectPhoto.src = '../img/projectPlaceholder.png';
        projectPhoto.alt = 'project photo';

        projectInfo.appendChild(projectName);
        projectInfo.appendChild(projectCategory);
        projectInfo.appendChild(charityName);
        projectInfo.appendChild(projectTime);
        projectInfo.appendChild(projectDescription);
        projectLink.appendChild(projectInfo);
        projectLink.appendChild(projectPhoto);
        li.appendChild(projectLink)
        projectsContainer.appendChild(li);
    });
}
document.addEventListener('DOMContentLoaded', function() {
  const menu = document.querySelector('.sidemenu');
  const toggleButton = document.querySelector('.sidemenu-button');


  toggleButton.onclick = function() {
    menu.classList.toggle('active');
    if(toggleButton.classList.toggle('active')){
      toggleButton.value = '<';
    } else {
      toggleButton.value = '>';
    }
  };
});


