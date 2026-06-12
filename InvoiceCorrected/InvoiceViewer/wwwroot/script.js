
document.addEventListener('DOMContentLoaded', function() {
    fetch('/api/invoice')
        .then(resp => {
            if (!resp.ok) {
                throw new Error(`HTTP error! Status: ${resp.status}`);
            }
            return resp.json();
        })
        .then(data => {
            let html = '<ul style="list-style-type: none; padding: 0;">';
            data.items.forEach(item => {
                html += `<li><strong>${item.name}</strong> - $${item.price}</li>`;
            });
            html += '</ul>';
            document.getElementById('invoice-container').innerHTML = html;
        })
        .catch(er => console.error("Failed to load invoice:", er));
});