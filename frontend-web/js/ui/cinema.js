document.addEventListener("DOMContentLoaded", initCinema);

function initCinema() {
  // TODO: read cinemaId from URL
  // TODO: load cinema details
  // TODO: load halls list
}

async function loadCinemaDetail(cinemaId) {
  // TODO: ApiService.getCinemaById(cinemaId)
  // TODO: render name/address
}

async function loadHalls(cinemaId) {
  // TODO: ApiService.getCinemaHalls()
  // TODO: filter halls by cinemaId on client
  // TODO: render halls
}