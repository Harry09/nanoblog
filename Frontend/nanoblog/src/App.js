import React, { Component } from "react";
import "./App.css";
import Home from "./views/Home";
import AuthService from "./AuthService";
import { Switch, Route } from "react-router-dom";
import Navbar from "./components/navbar";
import Register from "./views/Register";
import Login from "./views/Login";

class App extends Component {
  constructor() {
    super();
    this.authService = new AuthService();
  }

  renderHome = () => {
    return <Home auth={this.authService} />;
  };

  renderRegister = () => {
    if (this.authService.isAuthenticated()) {
      document.location.href = "/";
    }

    return <Register />;
  };

  renderLogin = () => {
    if (this.authService.isAuthenticated()) {
      document.location.href = "/";
    }

    return <Login auth={this.authService} />;
  };

  renderLoginButton() {
    let button = null;

    if (this.authService.isAuthenticated()) {
      button = (
        <button
          className="btn btn-secondary"
          onClick={() => this.authService.logout()}
        >
          Logout
        </button>
      );
    } else {
      button = (
        <React.Fragment>
          <button
            className="btn btn-secondary"
            onClick={() => (document.location.href = "/register")}
          >
            Register
          </button>
          <button
            className="btn btn-secondary"
            onClick={() => (document.location.href = "/login")}
          >
            Login
          </button>
        </React.Fragment>
      );
    }

    return button;
  }

  render() {
    let logoutButton = this.renderLoginButton();

    return (
      <div className="App">
        <Navbar>{logoutButton}</Navbar>

        <main role="main" className="container">
          <Switch>
            <Route exact path="/" render={this.renderHome} />
            <Route exact path="/register" render={this.renderRegister} />
            <Route exact path="/login" render={this.renderLogin} />
          </Switch>
        </main>

        {this.renderFooter()}
      </div>
    );
  }

  renderFooter() {
    return (
      <footer className="footer">
        <div className="container">
          <span className="text-muted">Piotr Krupa &copy; 2018</span>
        </div>
      </footer>
    );
  }
}

export default App;
