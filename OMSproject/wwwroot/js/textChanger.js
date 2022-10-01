const selectElement = documents.querySelector('selectName');

selectElement.addEventListener('change', (event) => {
    const result = document.querySelector('.result');
    result.textContent = 'You Likes ${event.target.value}';
}
)