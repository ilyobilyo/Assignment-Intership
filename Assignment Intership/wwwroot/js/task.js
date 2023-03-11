const statusBtns = document.querySelectorAll('.status');

statusBtns.forEach(b => b.addEventListener('click', onChange));

function onChange(e) {
	e.preventDefault();

	let id = "";
	let td;

	if (e.target.tagName == "I") {
		id = e.target.parentElement.parentElement.dataset.id;
		td = e.target.parentElement.parentElement;
		e.target.parentElement.remove();

	} else {
		id = e.target.parentElement.dataset.id;
		td = e.target.parentElement;
		e.target.remove();
    }

	fetch('/Task/ChangeTaskStatus/' + id)
		.then(response => response.json())
		.then(data => {

			let btn = document.createElement('button');
			btn.classList.add('btn');

			if (data.task.status == 'InProgress') {
				btn.classList.add('btn-warning');
				btn.dataset.id = data.task.id;

				const i = document.createElement('i');
				i.classList.add('fas');
				i.classList.add('fa-spinner');
				i.textContent = " In Progress";
				btn.appendChild(i);
			} else {
				btn.classList.add('btn-success');

				const i = document.createElement('i');
				i.classList.add('fas');
				i.classList.add('fa-check');
				i.textContent = " Completed";
				btn.appendChild(i);
				btn.disabled = true;
			}

			btn.addEventListener('click', onChange)
			td.appendChild(btn);
		})

}

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