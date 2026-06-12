document.addEventListener('DOMContentLoaded', function() {
    // Force it to point explicitly to your live Railway API endpoint
    // fetch('https://invoiceviewer-production-1090.up.railway.app/api/invoice')
    fetch('api/invoice')
        .then(resp => {
            if (!resp.ok) {
                throw new Error(`HTTP error! Status: ${resp.status}`);
            }
            return resp.json();
        })
        .then(data => {
            let html = '<ul style="list-style-type: none; padding: 0; margin: 0; font-size: 18px;">';
            data.items.forEach(item => {
                html += `<li style="padding: 5px 0;"><strong>${item.name}</strong> - $${item.price}</li>`;
            });
            html += '</ul>';
            document.getElementById('invoice-container').innerHTML = html;
        })
        .catch(er => {
            console.error("Failed to load invoice:", er);
            document.getElementById('invoice-container').innerHTML = `<p style="color: red;">Error loading invoice details.</p>`;
        });
});