document.addEventListener('DOMContentLoaded', function () {
    // === VARIABLES GLOBALES ===
    const regionSelect = document.getElementById('regionSelect');
    const localiteSelect = document.getElementById('localiteSelect');
    const nextButton = document.getElementById('nextButton');
    const prevButton = document.getElementById('prevButton');
    const sections = document.querySelectorAll('.form-section');
    const submitButton = document.getElementById('submitButton');

    const longueurField = document.getElementById('Longueur');
    const largeurField = document.getElementById('Largeur');
    const superficieField = document.getElementById('superficieField');

    const printGlobalButton = document.getElementById('printGlobalButton');
    const printIndividualButtons = document.querySelectorAll('.printIndividualButton');

    let currentSectionIndex = 0;

    // === GESTION DE LA NAVIGATION ENTRE LES FORMULAIRES ===
    function showSection(index) {
        sections.forEach((section, i) => {
            section.classList.toggle('d-none', i !== index);
        });
    }

    function updateButtons() {
        prevButton.disabled = currentSectionIndex === 0;
        nextButton.classList.toggle('d-none', currentSectionIndex === sections.length - 1);
        submitButton.classList.toggle('d-none', currentSectionIndex !== sections.length - 1);
    }

    nextButton?.addEventListener('click', function () {
        if (currentSectionIndex < sections.length - 1) {
            currentSectionIndex++;
            showSection(currentSectionIndex);
            updateButtons();
        }
    });

    prevButton?.addEventListener('click', function () {
        if (currentSectionIndex > 0) {
            currentSectionIndex--;
            showSection(currentSectionIndex);
            updateButtons();
        }
    });

    // === GESTION DU CHARGEMENT DES LOCALITÉS PAR RÉGION ===
    regionSelect?.addEventListener('change', function () {
        const regionId = this.value;
        localiteSelect.innerHTML = '<option value="">-- Sélectionnez une localité --</option>';
        if (regionId) {
            fetch(`/Localite/GetByRegion/${regionId}`)
                .then(response => {
                    if (!response.ok) throw new Error('Erreur lors du chargement des localités.');
                    return response.json();
                })
                .then(localites => {
                    localites.forEach(localite => {
                        const option = document.createElement('option');
                        option.value = localite.id;
                        option.textContent = localite.nom;
                        localiteSelect.appendChild(option);
                    });
                })
                .catch(error => {
                    alert('Erreur : ' + error.message);
                });
        }
    });

    // === CALCUL DYNAMIQUE DE LA SUPERFICIE ===
    function updateSuperficie() {
        const longueur = parseFloat(longueurField.value) || 0;
        const largeur = parseFloat(largeurField.value) || 0;
        superficieField.value = (longueur * largeur).toFixed(2);
    }

    longueurField?.addEventListener('input', updateSuperficie);
    largeurField?.addEventListener('input', updateSuperficie);

    // === IMPRESSION GLOBALE ===
    function printGlobal() {
        const tableContent = document.getElementById('personTable').outerHTML;

        const style = `
            <style>
                body { font-family: Arial, sans-serif; margin: 20px; }
                table { width: 100%; border-collapse: collapse; }
                th, td { border: 1px solid #ddd; padding: 8px; text-align: center; }
                th { background-color: #f2f2f2; }
                img { max-width: 100px; height: auto; }
            </style>
        `;

        const printWindow = window.open('', '', 'height=700, width=900');
        printWindow.document.write('<html><head><title>Liste des personnes</title>' + style + '</head><body>');
        printWindow.document.write('<h2 style="text-align:center;">Liste des personnes</h2>');

        // Retirer la colonne Actions (boutons)
        const table = document.createElement('table');
        table.innerHTML = tableContent;
        const actionsColumn = table.querySelectorAll('th:last-child, td:last-child');
        actionsColumn.forEach(cell => cell.remove());

        printWindow.document.write(table.outerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
    }

    // === IMPRESSION INDIVIDUELLE AVEC NUMÉRO DE DOSSIER ===
    function printIndividual(button) {
        const row = button.closest('tr').cloneNode(true);

        // Supprimer la colonne Actions (boutons)
        row.querySelector('td:last-child').remove();

        // Remplacer les montants par "XOF"
        const montantCells = row.querySelectorAll('td');
        montantCells.forEach((cell) => {
            if (cell.textContent.includes('$')) {
                cell.textContent = cell.textContent.replace(/\$/g, 'XOF');
            }
        });

        // Collecte des informations de la ligne pour la fiche individuelle
        const photoSrc = row.querySelector('img').src;
        const nom = row.children[0].textContent;
        const prenom = row.children[1].textContent;
        const surnom = row.children[2].textContent;
        const contact = row.children[3].textContent;
        const superficie = row.children[4].textContent;
        const prixM2 = row.children[5].textContent.replace(/\$/g, 'XOF');
        const montant = row.children[6].textContent.replace(/\$/g, 'XOF');
        const commentaire = "Pas de commentaire"; // Champ par défaut
        const numeroDossier = row.children[7].textContent;

        // Style de la fiche individuelle 
        const style = `
            <style>
                body { font-family: Arial, sans-serif; margin: 30px; }
                .fiche-container { text-align: center; margin-bottom: 20px; padding: 20px; border: 1px solid #ddd; border-radius: 10px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1); width: 80%; margin: 0 auto; }
                .fiche-photo { width: 120px; height: 120px; object-fit: cover; border: 2px solid #ccc; border-radius: 8px; margin-bottom: 20px; }
                .fiche-title { font-size: 1.8em; margin-bottom: 20px; color: #444; }
                .info-section { display: flex; flex-wrap: wrap; justify-content: space-between; gap: 15px; margin-bottom: 20px; }
                .info-item { flex: 0 0 48%; font-size: 1.1em; padding: 8px; border-bottom: 1px dashed #ddd; }
                .info-item span { font-weight: bold; color: #333; }
                .info-header { font-size: 1.5em; margin-top: 20px; text-align: center; }
            </style>
        `;

        // Contenu de la fiche individuelle
        const content = `
            <div class="fiche-container">
                <h2 class="fiche-title">Fiche Individuelle</h2>
                <img src="${photoSrc}" alt="Photo" class="fiche-photo" />
                <div class="info-section">
                    <div class="info-item"><span>Nom :</span> ${nom}</div>
                    <div class="info-item"><span>Prénom :</span> ${prenom}</div>
                    <div class="info-item"><span>Surnom :</span> ${surnom}</div>
                    <div class="info-item"><span>Contact :</span> ${contact}</div>
                    <div class="info-item"><span>Superficie :</span> ${superficie}</div>
                    <div class="info-item"><span>Prix/M² :</span> ${prixM2}</div>
                    <div class="info-item"><span>Montant Total :</span> ${montant}</div>
                    <div class="info-item"><span>Numéro de Dossier :</span> ${numeroDossier}</div>
                    <div class="info-item"><span>Commentaire :</span> ${commentaire}</div>
                </div>
                <div class="info-header">Fiche complète</div>
            </div>
        `;

        const printWindow = window.open('', '', 'height=700, width=900');
        printWindow.document.write('<html><head><title>Fiche Individuelle</title>' + style + '</head><body>');
        printWindow.document.write(content);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
    }

    // === EVENEMENTS ===
    printGlobalButton?.addEventListener('click', printGlobal);
    printIndividualButtons?.forEach(button => {
        button.addEventListener('click', function () {
            printIndividual(this);
        });
    });
});
