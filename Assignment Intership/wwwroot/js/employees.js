


const div = document.querySelector('.page');

let page = Number(div.children[1].textContent.split('/')[0]);
const totalPages = div.children[1].textContent.split('/')[1];

if(page > 1){
	let prevPage = page - 1;
	div.children[0].href = '?pageNumber=' + prevPage;
	div.children[0].style.display = 'inline'
}

if(page < totalPages){
	let nextPage = page + 1;
	div.children[2].href = '?pageNumber=' + nextPage;
	div.children[2].style.display = 'inline'
}


const btns = document.querySelectorAll('.btn-danger');

btns.forEach(b => b.addEventListener('click', async (e) => {
	e.preventDefault();
	const id = e.target.dataset.id;
	const response = await fetch('/Employee/Delete/' + id, {
		method: 'delete'
	});

	if(response.ok){
		e.target.parentElement.parentElement.remove();
	}

}))

const sortElement = document.querySelector('.sort').addEventListener('click', (e) => {
	const body = document.querySelector('tbody');
	const rows = document.querySelectorAll('tbody tr');

	if (e.target.className == 'fas fa-arrow-down') {
		const sort = Array.from(rows).sort((a, b) => a.children[1].textContent.localeCompare(b.children[1].textContent));
		sort.forEach(x => {
			body.appendChild(x);
		})

		e.target.classList.remove('fa-arrow-down');
		e.target.classList.add('fa-arrow-up');
	} else {
		const sort = Array.from(rows).sort((a, b) => b.children[1].textContent.localeCompare(a.children[1].textContent));
		sort.forEach(x => {
			body.appendChild(x);
		})

		e.target.classList.remove('fa-arrow-up');
		e.target.classList.add('fa-arrow-down');
    }
	
});


const sortSalaryElement = document.querySelector('.sortSalary').addEventListener('click', (e) => {
	const body = document.querySelector('tbody');
	const rows = document.querySelectorAll('tbody tr');

	if (e.target.className == 'fas fa-arrow-down') {
		const sort = Array.from(rows).sort((a, b) => Number(a.children[5].querySelector('.value').textContent) - Number(b.children[5].querySelector('.value').textContent));
		sort.forEach(x => {
			body.appendChild(x);
		})

		e.target.classList.remove('fa-arrow-down');
		e.target.classList.add('fa-arrow-up');
	} else {
		const sort = Array.from(rows).sort((a, b) => Number(b.children[5].querySelector('.value').textContent) - Number(a.children[5].querySelector('.value').textContent));
		sort.forEach(x => {
			body.appendChild(x);
		})

		e.target.classList.remove('fa-arrow-up');
		e.target.classList.add('fa-arrow-down');
	}

});
