export default class AuthService {
  isUpdating = false;

  login(login, password, onError) {
    fetch("/api/accounts/login", {
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      method: "POST",
      body: JSON.stringify({
        email: login,
        password: password
      })
    }).then(response => {
      response.json().then(json => {
        if (!response.ok) {
          onError(json.message);
        } else {
          this.setSession(json);
          window.location.href = "/";
        }
      });
    });
  }

  logout() {
    fetch(
      "/api/accounts/tokens/revoke/" + localStorage.getItem("refresh_token")
    );

    localStorage.removeItem("access_token");
    localStorage.removeItem("expires_at");
    localStorage.removeItem("refresh_token");
    window.location.href = "/";
  }

  setSession(authResult) {
    localStorage.setItem("access_token", authResult.token);
    localStorage.setItem("refresh_token", authResult.refreshToken);
    localStorage.setItem("expires_at", authResult.expires);
  }

  refreshToken(onSuccess) {
    fetch(
      "/api/accounts/tokens/refresh/" + localStorage.getItem("refresh_token")
    ).then(response => {
      response.json().then(json => {
        if (!response.ok) {
          console.log("Cannot refresh token!", json);
          this.logout();
        } else {
          console.log("Got new access token! :D");
          this.setSession(json);
          onSuccess();
        }
      });
    });
  }

  tryRefreshToken(onSuccess) {
    if (
      (localStorage.getItem("refresh_token") !== null) &
      !this.isAuthenticated()
    ) {
      this.refreshToken(onSuccess);

      return true;
    }

    return false;
  }

  isAuthenticated() {
    let expiresAt = JSON.parse(localStorage.getItem("expires_at"));
    return new Date().getTime() < expiresAt;
  }

  getUserId() {
    var jwtDecode = require("jwt-decode");

    const accessToken = localStorage.getItem("access_token");

    if (accessToken) {
      var token = jwtDecode(this.getAccessToken());

      return token.sub;
    }

    return null;
  }

  getAccessToken() {
    const accessToken = localStorage.getItem("access_token");
    if (!accessToken) {
      throw new Error("No access token found");
    }
    return accessToken;
  }
}
