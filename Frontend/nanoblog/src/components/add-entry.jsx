import React, { Component } from "react";

import "./add-entry.css";

class AddEntry extends Component {
  state = {
    text: ""
  };

  handleChange = event => {
    this.setState({ text: event.target.value });
  };

  handleSubmit = event => {
    event.preventDefault();

    this.props.onSubmit(this.state.text);

    this.setState({ text: "" });
  };

  render() {
    return (
      <React.Fragment>
        <h3>Dodaj post</h3>
        <form onSubmit={this.handleSubmit}>
          <div className="form-group">
            <textarea
              value={this.state.text}
              onChange={this.handleChange}
              id="input-text-form"
              rows="5"
            />
          </div>

          <button type="submit" className="btn btn-primary">
            Dodaj
          </button>
        </form>
      </React.Fragment>
    );
  }
}

export default AddEntry;
