if (!window.localiteInit) {
    window.localiteInit = true;
    // Gestion des sections du formulaire
    document.addEventListener('DOMContentLoaded', function () {
        const regionSelect = document.getElementById('regionSelect');
        const localiteSelect = document.getElementById('localiteSelect');
        const nextButton = document.getElementById('nextButton');
        const prevButton = document.getElementById('prevButton');
        const sections = document.querySelectorAll('.form-section');
        let currentSectionIndex = 0;

        // Afficher la première section et masquer les autres
        sections[currentSectionIndex].classList.remove('d-none');

        // Fonction pour mettre à jour les boutons Prev et Next
        function updateButtons() {
            prevButton.disabled = currentSectionIndex === 0;
            if (currentSectionIndex === sections.length - 1) {
                nextButton.disabled = true;
                document.getElementById('submitButton').classList.remove('d-none');
            } else {
                nextButton.disabled = false;
                document.getElementById('submitButton').classList.add('d-none');
            }
        }

        // Gestion du clic sur le bouton "Next"
        nextButton.addEventListener('click', function () {
            if (currentSectionIndex < sections.length - 1) {
                sections[currentSectionIndex].classList.add('d-none');
                currentSectionIndex++;
                sections[currentSectionIndex].classList.remove('d-none');
                updateButtons();
            }
        });

        // Gestion du clic sur le bouton "Prev"
        prevButton.addEventListener('click', function () {
            if (currentSectionIndex > 0) {
                sections[currentSectionIndex].classList.add('d-none');
                currentSectionIndex--;
                sections[currentSectionIndex].classList.remove('d-none');
                updateButtons();
            }
        });

        // Fonction pour charger les localités en fonction de la région sélectionnée
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
                        alert(error.message);
                    });
            }
        });

        // Initialisation des boutons en fonction de la section affichée
        updateButtons();
    });

    // Calcul dynamique de la superficie
    document.addEventListener('DOMContentLoaded', function () {
        const longueurField = document.querySelector('[asp-for="Longueur"]');
        const largeurField = document.querySelector('[asp-for="Largeur"]');
        const superficieField = document.getElementById('superficieField');

        function updateSuperficie() {
            const longueur = parseFloat(longueurField.value) || 0;
            const largeur = parseFloat(largeurField.value) || 0;
            superficieField.value = (longueur * largeur).toFixed(2);
        }

        longueurField.addEventListener('input', updateSuperficie);
        largeurField.addEventListener('input', updateSuperficie);
    });
}
