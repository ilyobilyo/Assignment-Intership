
	const completeBtns = document.querySelectorAll('.complete');

	completeBtns.forEach(b => b.addEventListener('click', async (e) => {
		e.preventDefault();
		const id = e.target.dataset.id;

		const td = e.target.parentElement;

		const response = await fetch('/Task/Complete/' + id);

		if (response.ok) {
			e.target.remove();

			let btn = document.createElement('button');
			btn.classList.add('btn');
			btn.classList.add('btn-secondary');
			btn.disabled = true;
			btn.textContent = 'Completed';

			td.appendChild(btn);
		}

	}))



	const div = document.querySelector('.page');

	let page = Number(div.children[1].textContent.split('/')[0]);
	const totalPages = div.children[1].textContent.split('/')[1];

	if (page > 1) {
		let prevPage = page - 1;
		div.children[0].href = '?pageNumber=' + prevPage;
		div.children[0].style.display = 'inline'
	}

	if (page < totalPages) {
		let nextPage = page + 1;
		div.children[2].href = '?pageNumber=' + nextPage;
		div.children[2].style.display = 'inline'
	}


	const deleteBtns = document.querySelectorAll('.btn-danger');

	deleteBtns.forEach(b => b.addEventListener('click', async (e) => {
		e.preventDefault();
		const id = e.target.dataset.id;
		const response = await fetch('/Task/Delete/' + id, {
			method: 'delete'
		});

		if (response.ok) {
			e.target.parentElement.parentElement.remove();
		}

	}))

