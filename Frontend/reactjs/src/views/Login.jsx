import React, { Component } from "react";

class Login extends Component {
  state = {
    email: "",
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

    console.log(JSON.stringify(this.state));

    this.props.auth.login(this.state.email, this.state.password, error => {
      this.setState({ error: error });
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
      <form onSubmit={this.handleFormSubmit} id="login-form">
        {this.renderMessage()}

        <div className="form-group">
          <label htmlFor="input-email-form">E-mail</label>
          <input
            type="email"
            id="input-email-form"
            className="form-control"
            name="email"
            placeholder="E-mail"
            value={this.state.email}
            onChange={this.handleUserInput}
          />
        </div>

        <div className="form-group">
          <label htmlFor="input-password-form">Hasło</label>
          <input
            type="password"
            id="input-password-form"
            className="form-control"
            name="password"
            placeholder="Hasło"
            value={this.state.password}
            onChange={this.handleUserInput}
          />
        </div>

        <button type="submit" className="btn btn-primary" name="submit">
          Zaloguj się
        </button>
      </form>
    );
  }
}

export default Login;
