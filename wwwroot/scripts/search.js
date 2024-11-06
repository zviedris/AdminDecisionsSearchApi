document.getElementById('searchForm').addEventListener('submit', function(event) {
    event.preventDefault();  // Prevent form from submitting normally

    const name = document.getElementById('name').value;
    const nmrCode = document.getElementById('nmrCode').value;

    // Call the API with the name and nmrCode
    fetch(`http://localhost:5005/api/CsvSearch/search?name=${encodeURIComponent(name)}&nmrCode=${encodeURIComponent(nmrCode)}`)
        .then(response => response.json())
        .then(data => {
            const resultDiv = document.getElementById('result');
            resultDiv.innerHTML = ''; // Clear previous results

            // Check if there is a message or data
            if (data.message) {
                resultDiv.innerHTML = `<p>${data.message}</p>`;
            } else {
                // Display the results in a table
                const table = document.createElement('table');
                table.innerHTML = `
                    <tr>
                        <th>Name</th>
                        <th>NMR Code</th>
                        <th>Date From</th>
                        <th>Date Till</th>
                        <th>Explanation</th>
                    </tr>
                `;
                data.forEach(record => {
                    const row = document.createElement('tr');
                    row.innerHTML = `
                        <td>${record.name}</td>
                        <td>${record.nmrCode}</td>
                        <td>${new Date(record.dateFrom).toLocaleDateString()}</td>
                        <td>${new Date(record.dateTill).toLocaleDateString()}</td>
                        <td>${record.explanation}</td>
                    `;
                    table.appendChild(row);
                });
                resultDiv.appendChild(table);
            }
        })
        .catch(error => {
            const resultDiv = document.getElementById('result');
            resultDiv.innerHTML = `<p>Error fetching data: ${error.message}</p>`;
        });
});
