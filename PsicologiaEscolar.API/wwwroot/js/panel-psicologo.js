const apiBaseUrl = "http://localhost:5214/api/ticket";

const usuario = JSON.parse(sessionStorage.getItem("usuario"));

if (!usuario || usuario.rol !== "Psicologo") {
    window.location.href = "login.html";
}

if (usuario.nombre) {
    document.getElementById("bienvenida").textContent = `Bienvenido/a, ${usuario.nombre}`;
}

document.getElementById("cerrarSesion").addEventListener("click", () => {
    sessionStorage.removeItem("usuario");
    window.location.href = "index.html";
});

document.addEventListener("DOMContentLoaded", function () {
    const tablaTickets = document.getElementById("ticketsTable").querySelector("tbody");
    const respuestaModal = new bootstrap.Modal(document.getElementById("respuestaModal"));
    const formRespuesta = document.getElementById("formRespuesta");
    const cerrarSesionBtn = document.getElementById("cerrarSesion");

    cargarTickets();

    cerrarSesionBtn.addEventListener("click", () => {
        fetch("http://localhost:5214/api/auth/logout", {
            method: "POST",
            credentials: "include"
        })
            .then(res => res.json())
            .then(() => {
                window.location.href = "index.html";
            });
    });

    formRespuesta.addEventListener("submit", function (e) {
        e.preventDefault();

        const id = document.getElementById("ticketId").value;
        const estado = document.getElementById("estadoSelect").value;
        const respuesta = document.getElementById("respuestaText").value;

        fetch("http://localhost:5214/api/ticket/actualizar", {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            credentials: "include",
            body: JSON.stringify({
                ticketId: parseInt(id),
                estado: estado,
                respuesta: respuesta
            })
        })
            .then(res => res.json())
            .then(data => {
                alert(data.mensaje);
                respuestaModal.hide();
                cargarTickets();
            });
    });

    function cargarTickets() {
        tablaTickets.innerHTML = "<tr><td colspan='8'>Cargando...</td></tr>";

        fetch("http://localhost:5214/api/ticket/AllTickets", {
            method: "GET",
            credentials: "include"
        })
            .then(res => {
                if (!res.ok) throw new Error("No autorizado o error en la solicitud");
                return res.json();
            })
            .then(tickets => {
                tablaTickets.innerHTML = "";

                tickets.forEach(ticket => {
                    const tr = document.createElement("tr");
                    tr.innerHTML = `
                    <td>${ticket.ticketId}</td>
                    <td>${ticket.idUsuario}</td>
                    <td>${ticket.estado}</td>
                    <td>${ticket.fechaCreacion}</td>
                    <td>${ticket.descripcion}</td>
                    <td>${ticket.respuesta || ""}</td>
                    <td>${ticket.fechaRespuesta || ""}</td>
                    <td>
                        <button class="btn btn-primary btn-sm me-1" onclick="abrirModal(${ticket.ticketId}, '${ticket.estado}', '${ticket.respuesta || ""}' )">Responder</button>
                        <button class="btn btn-danger btn-sm" onclick="eliminarTicket(${ticket.ticketId})">Eliminar</button>
                    </td>
                `;
                    tablaTickets.appendChild(tr);
                });
            })
            .catch(err => {
                tablaTickets.innerHTML = `<tr><td colspan='8'>Error: ${err.message}</td></tr>`;
            });
    }

    window.abrirModal = function (id, estado, respuesta) {
        document.getElementById("ticketId").value = id;
        document.getElementById("estadoSelect").value = estado;
        document.getElementById("respuestaText").value = respuesta;
        respuestaModal.show();
    };

    window.eliminarTicket = function (id) {
        if (confirm("¿Estás seguro de que deseas eliminar este ticket?")) {
            fetch(`http://localhost:5214/api/ticket/${id}`, {
                method: "DELETE",
                credentials: "include"
            })
                .then(res => res.json())
                .then(data => {
                    alert(data.mensaje);
                    cargarTickets();
                });
        }
    };
});

