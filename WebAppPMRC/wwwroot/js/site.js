document.addEventListener('DOMContentLoaded', function () {
    // Sélection des éléments du formulaire
    const regionSelect = document.getElementById('regionSelect');
    const localiteSelect = document.getElementById('localiteSelect');
    const nextButton = document.getElementById('nextButton');
    const prevButton = document.getElementById('prevButton');
    const sections = document.querySelectorAll('.form-section');
    const submitButton = document.getElementById('submitButton');

    const longueurField = document.getElementById('Longueur');
    const largeurField = document.getElementById('Largeur');
    const superficieField = document.getElementById('superficieField');

    let currentSectionIndex = 0;

    // Afficher la première section et masquer les autres
    function showSection(index) {
        sections.forEach((section, i) => {
            if (i === index) {
                section.classList.remove('d-none');
            } else {
                section.classList.add('d-none');
            }
        });
    }

    // Mettre à jour les boutons Prev, Next et Submit
    function updateButtons() {
        prevButton.disabled = currentSectionIndex === 0;

        if (currentSectionIndex === sections.length - 1) {
            nextButton.classList.add('d-none');
            submitButton.classList.remove('d-none');
        } else {
            nextButton.classList.remove('d-none');
            submitButton.classList.add('d-none');
        }
    }

    // Gestion du bouton "Next"
    nextButton.addEventListener('click', function () {
        if (currentSectionIndex < sections.length - 1) {
            currentSectionIndex++;
            showSection(currentSectionIndex);
            updateButtons();
        }
    });

    // Gestion du bouton "Prev"
    prevButton.addEventListener('click', function () {
        if (currentSectionIndex > 0) {
            currentSectionIndex--;
            showSection(currentSectionIndex);
            updateButtons();
        }
    });

    // Gestion du changement de région pour charger les localités
    regionSelect.addEventListener('change', function () {
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

    // Calcul dynamique de la superficie
    function updateSuperficie() {
        const longueur = parseFloat(longueurField.value) || 0;
        const largeur = parseFloat(largeurField.value) || 0;
        superficieField.value = (longueur * largeur).toFixed(2);
    }

    longueurField.addEventListener('input', updateSuperficie);
    largeurField.addEventListener('input', updateSuperficie);

    // Initialisation
    showSection(currentSectionIndex);
    updateButtons();
});
