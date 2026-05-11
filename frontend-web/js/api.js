const BASE_URL = "http://localhost:5106/api"; // but maybe :7200 and we will use environment variable in real app

const ApiService = {
  async getMovies(genre) {
    // TODO: GET /api/movies?genre=
  },
  async getMovieById(movieId) {
    // TODO: GET /api/movies/{id}
  },
  async getSessions(movieId) {
    // TODO: GET /api/sessions?movieId=
  },
  async getSeatMap(sessionId) {
    // TODO: GET /api/seats/session/{id}
  },
  async createBooking(payload) {
    // TODO: POST /api/bookings
  },
   async getCinemas() {
    // TODO: GET /api/cinemas
  },
  async getCinemaById(cinemaId) {
    // TODO: GET /api/cinemas/{id}
  },
  async getCinemaHalls() {
    // TODO: GET /api/cinemahalls
  },
  async getHallById(hallId) {
    // TODO: GET /api/cinemahalls/{id}
  },
  async getRecommendations(genre) {
    // TODO: GET /api/recommendations/{genre}
  }
};