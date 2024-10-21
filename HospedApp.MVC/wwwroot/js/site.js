document.getElementById('searchInput').addEventListener('keyup', function () {
    const searchValue = this.value.toLowerCase();
    const table = document.querySelector('.table tbody');
    const rows = table.querySelectorAll('tr');

    rows.forEach(row => {
        const cells = row.querySelectorAll('td');
        let rowContainsSearchValue = false;
        for (let i = 0; i < cells.length; i++) {
            const cellText = cells[i].textContent.toLowerCase();
            if (cellText.includes(searchValue)) {
                rowContainsSearchValue = true;
                break;
            }
        }

        row.style.display = rowContainsSearchValue ? '' : 'none';
    });
});
