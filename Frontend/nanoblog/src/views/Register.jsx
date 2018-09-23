import React, { Component } from "react";

class Register extends Component {
  state = {
    email: "",
    username: "",
    password: "",
    error: ""
  };

  handleUserInput = e => {
    const name = e.target.name;
    const value = e.target.value;
    this.setState({ [name]: value });
  };

  handleFormSubmit = event => {
    event.preventDefault();

    console.log(
      JSON.stringify({
        email: this.state.email,
        username: this.state.username,
        password: this.state.password
      })
    );

    fetch("/api/accounts/register", {
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      method: "POST",
      body: JSON.stringify(this.state)
    }).then(response => {
      if (!response.ok) {
        response.json().then(json => {
          this.setState({ error: json.message });
        });
      }
    });
  };

  renderMessage() {
    if (this.state.error.length > 0) {
      return (
        <React.Fragment>
          <br />
          <div className="alert alert-danger" role="alert">
            {this.state.error}
          </div>
        </React.Fragment>
      );
    }
  }

  render() {
    return (
      <form onSubmit={this.handleFormSubmit}>
        {this.renderMessage()}

        <div className="form-group">
          <label>E-mail</label>
          <input
            type="email"
            className="form-control"
            name="email"
            value={this.state.email}
            onChange={event => this.handleUserInput(event)}
            placeholder="E-mail"
          />
        </div>
        <div className="form-group">
          <label>Nazwa użytkownika</label>
          <input
            type="text"
            className="form-control"
            name="username"
            value={this.state.username}
            onChange={event => this.handleUserInput(event)}
            placeholder="Nazwa użytkownika"
          />
        </div>
        <div className="form-group">
          <label>Hasło</label>
          <input
            type="password"
            className="form-control"
            name="password"
            value={this.state.password}
            onChange={event => this.handleUserInput(event)}
            placeholder="Hasło"
          />
        </div>

        <button type="submit" className="btn btn-primary" name="submit">
          Zarejestruj się
        </button>
      </form>
    );
  }
}

export default Register;
