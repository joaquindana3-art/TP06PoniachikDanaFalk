const palabra = document.getElementById("palabra")
limpiar()
function ArriesgarLetra() {

    const letra = document.getElementById("letra")
    let palabraOculta = document.getElementById("palabraOculta")
    let intentos = document.getElementById("intentos")
    let error = document.getElementById("error")
    let encontrado = false
    if (isNaN(letra.value) && letra.value.length === 1) {

        error.innerHTML = ""
        for (let i = 0; i < palabra.value.length; i++) {

            if (palabra.value[i] === letra.value.toUpperCase()) {

                let letraMostrada  = palabraOculta.innerHTML.split("")
                letraMostrada[i] = letra.value.toUpperCase()
                palabraOculta.innerHTML = letraMostrada.join("")
                encontrado = true

            }

        }
        if (!encontrado) {

            intentos.innerHTML--

        }

    }
    else {

        error.innerHTML = "Por favor ingresa una letra, cualquier otro caracter o una distinta cantidad de letras es invalido."
        error.style.color = "red";

    }
    resultado(palabraOculta, intentos)

}
function resultado(palabraOculta, intentos) {
    let jugarDeNuevo = document.getElementById("jugarDeNuevo");
    let resultado = document.getElementById("resultado");
    let divIntentos = document.getElementById("divIntentos");
    const titulo = document.getElementById("titulo");
    const descripcion = document.getElementById("descripcion");
    let divLetras = document.getElementById("divLetras");
    let segundaBtn = document.getElementById("segundaHabitacion");

    if (!palabraOculta.innerHTML.includes("_")) {
        resultado.innerHTML = "Ganaste";
        resultado.style.color = "lightgreen";
        document.querySelector("main").style.backgroundColor = "darkgreen";
        descripcion.style.color = "white";
        palabraOculta.style.color = "white";
        divIntentos.style.color = "white";
        divLetras.style.color = "white";
    }

    if (intentos.innerHTML < 0) {
        resultado.innerHTML = "Perdiste. ";
        intentos.innerHTML = "NO TE QUEDAN INTENTOS";
        palabraOculta.style.color = "white";
        intentos.style.color = "white";
        resultado.style.color = "pink";
        document.querySelector("main").style.backgroundColor = "darkred";
        descripcion.style.color = "white";
        palabraOculta.style.color = "white";
        divIntentos.style.color = "white";
        divLetras.style.color = "white";
    }
}


function limpiar() {
    let intentos = document.getElementById("intentos");
    let palabraOculta = document.getElementById("palabraOculta");
    intentos.innerHTML = 5;
    palabraOculta.innerHTML = "";
    for (let i = 0; i < palabra.value.length; i++) {
        palabraOculta.innerHTML += "_";
    }
}
















function ArriesgarPalabra() {

    const respuesta = document.getElementById("respuesta")
    let palabraRosco = document.getElementById("palabraRosco")
    let cantCorrectas = document.getElementById("cantPalabrasCorrectas")
    let cantIncorrectas = document.getElementById("cantPalabrasIncorrectas")
    if (respuesta.value.toUpperCase() === palabraRosco.value.toUpperCase()) {

        cantCorrectas.innerHTML++

    }
    else {
        cantIncorrectas.innerHTML++
    }

}
    if (respuesta.value.toUpperCase() === palabraRosco.value.toUpperCase()) {
        resultado.innerHTML = "Ganaste";
        resultado.style.color = "lightgreen";
        document.querySelector("main").style.backgroundColor = "darkgreen";
        descripcion.style.color = "white";
        palabraOculta.style.color = "white";
        divIntentos.style.color = "white";
        divLetras.style.color = "white";
    }