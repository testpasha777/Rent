(function () {
    $(function () {
        var form = document.getElementById("gallery");
        var typeOfHousing = document.getElementById("TypeOfHousingId");
        var fletNumberlabel = document.getElementById("fletNumberlabel");
        var fletNumber = document.getElementById("fletNumber");
        var grid = document.getElementById('grid');
        $('.chosen-select').chosen();
        $('chosen-select-deselect').chosen({ allow_single_deselect: true });
        var input = document.getElementById('autocomplete');
        var autocomplete = new google.maps.places.Autocomplete(input, { types: ['(cities)'] });

        function readURL(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    var img = document.createElement("img");
                    var input = document.createElement("textarea");
                    var div = document.createElement('div');
                    var button = document.createElement('div');
                    var a = document.createElement('a');

                    button.style.textAlign = 'center';
                    button.style.backgroundColor = 'black';

                    a.innerText = "DELETE";
                    a.addEventListener('click', function () {
                    this.parentNode.parentNode.parentNode.removeChild(this.parentNode.parentNode);

                    });

                    button.appendChild(a);

                    div.classList.add('cell');

                    img.classList.add('responsive-image');
                    img.src = e.target.result;
                    div.appendChild(img);
                    div.appendChild(button);

                    input.name = "images";
                    input.style = "display: none";
                    input.textContent = e.target.result;
                    div.appendChild(input);
                    grid.appendChild(div);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }

        google.maps.event.addListener(autocomplete, 'place_changed', function () {
            var place = autocomplete.getPlace();
            console.log(place);
        })

        document.getElementById("addImg").addEventListener('change', function () {
                readURL(this);
            this.value = '';
        });

        $("#createApartmentForm").keypress(function (e) {
            keycode = e.keyCode || e.charCode || e.which //for cross browser
            if (keycode == 13)    //keyCode for enter key
            {
                return false;
            }
        });

    })();
})();