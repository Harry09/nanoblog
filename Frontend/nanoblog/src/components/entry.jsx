import React, { Component } from "react";

import "./entry.css";

class Entry extends Component {
  handleDelete = event => {
    event.preventDefault();

    this.props.onDelete(this.props.dataKey);
  };

  renderDeleteButton() {
    if (this.props.auth.isAuthenticated()) {
      var jwtDecode = require("jwt-decode");

      var token = jwtDecode(this.props.auth.getAccessToken());

      if (token.sub === this.props.entryId) {
        return (
          <div className="post-delete" onClick={this.handleDelete}>
            <a className="post-delete-link" href="">
              Usuń
            </a>
          </div>
        );
      }
    }
  }

  render() {
    return (
      <div className="post">
        <div className="post-header">
          <div className="post-author">
            <a href="">{this.props.author}</a>
          </div>

          <div className="post-date">
            <a href="">{this.props.date}</a>
          </div>

          {this.renderDeleteButton()}
        </div>

        <div className="post-text">{this.props.children}</div>
      </div>
    );
  }
}

export default Entry;
