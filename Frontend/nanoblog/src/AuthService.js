export default class AuthService {
  login() {
    fetch("/api/accounts/token")
      .then(res => res.json())
      .then(result => {
        if (result !== undefined) {
          this.setSession(result);
          console.log("Sucessfully got token");
          window.location.href = "/";
        } else {
          console.log("Cannot get token!");
        }
      });
  }

  logout() {
    localStorage.removeItem("access_token");
    localStorage.removeItem("expires_at");
    window.location.href = "/";
  }

  setSession(authResult) {
    localStorage.setItem("access_token", authResult.token);
    localStorage.setItem("expires_at", authResult.expires);
  }

  isAuthenticated() {
    let expiresAt = JSON.parse(localStorage.getItem("expires_at"));
    console.log(new Date().getTime(), expiresAt);

    return new Date().getTime() < expiresAt;
  }

  getAccessToken() {
    const accessToken = localStorage.getItem("access_token");
    if (!accessToken) {
      throw new Error("No access token found");
    }
    return accessToken;
  }
}
