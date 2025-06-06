// Verifica si ya hay una sesión activa
const usuario = JSON.parse(sessionStorage.getItem("usuario"));

if (usuario && usuario.rol) {
    if (usuario.rol === "Docente") {
        window.location.href = "panel-docente.html";
    } else if (usuario.rol === "Psicologo") {
        window.location.href = "panel-psicologo.html";
    } else if (usuario.rol === "Administrativo") {
        window.location.href = "panel-docente.html"; // Cambia si tienes otra vista
    }
}

//Lógica para leer los datos recibidos por el form y enviarlos al backend

document.getElementById("loginForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    const respuesta = await fetch("/api/auth/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password }),
    });

    const mensajeError = document.getElementById("mensajeError");

    if (!respuesta.ok) {
        mensajeError.textContent = "Correo o contraseña inválidos.";
        return;
    }

    const datos = await respuesta.json();
    sessionStorage.setItem("usuario", JSON.stringify(datos));

    if (datos.rol === "Docente" ) {
        window.location.href = "panel-docente.html";
    } else if (datos.rol === "Psicologo") {
        window.location.href = "panel-psicologo.html";
    } else if (datos.rol === "Administrativo") {
        window.location.href = "panel-docente.html";
    } else {
        mensajeError.textContent = "Rol no reconocido.";
    }
});