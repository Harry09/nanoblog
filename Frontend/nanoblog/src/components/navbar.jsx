import React, { Component } from "react";

import "./navbar.css";

class Navbar extends Component {
  state = {
    userInfo: {},
    isLoaded: false
  };

  componentDidMount() {
    var userId = this.props.auth.getUserId();
    this.setState({ isLoaded: false });

    if (userId !== null) {
      fetch("/api/accounts/user/" + userId).then(response => {
        if (response.ok) {
          response.json().then(json => {
            this.setState({ userInfo: json, isLoaded: true });
          });
        }
      });
    }
  }

  render() {
    return (
      <nav className="navbar navbar-expand navbar-dark bg-dark">
        <a className="navbar-brand" href="">
          Nanoblog
        </a>
        <button
          className="navbar-toggler"
          type="button"
          data-toggle="collapse"
          data-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon" />
        </button>
        <div className="collapse navbar-collapse">
          <ul className="navbar-nav mr-auto">
            <li className="nav-item active">
              <a className="nav-link" href="">
                Nanoblog
                <span className="sr-only">(current)</span>
              </a>
            </li>
          </ul>
        </div>

        {this.renderLoginButton()}
      </nav>
    );
  }

  renderLoginButton() {
    let button = null;

    if (this.props.auth.isAuthenticated()) {
      var welcome;

      if (this.state.isLoaded) {
        welcome = (
          <span className="navbar-text user-welcome">
            Witaj, <a href="">{this.state.userInfo.userName}</a>
          </span>
        );
      }

      button = (
        <React.Fragment>
          {welcome}

          <button
            className="btn btn-secondary"
            onClick={() => this.props.auth.logout()}
          >
            Logout
          </button>
        </React.Fragment>
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
}

export default Navbar;
