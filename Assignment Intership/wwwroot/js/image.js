window.addEventListener('load', setImage);

function setImage() {
	if (window.location.pathname.length > 10) {
		const imgElement = document.querySelector('img');
		const img = getImage(imgElement.dataset.id)
			.then(i => imgElement.setAttribute('src', 'data:img/png;base64,' + i.photo));

	} else {
		const rows = document.querySelectorAll('tbody tr');

		rows.forEach(x => {
			const img = getImage(x.dataset.id)
				.then(i => x.children[0].children[0].setAttribute('src', 'data:img/png;base64,' + i.photo));
		})
    }
	
}

async function getImage(id) {
	const response = await fetch('/Employee/GetEmployeeImage/' + id);
	const data = await response.json();

	return data;
}