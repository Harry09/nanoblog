import React, { Component } from "react";
import Entries from "../components/entries";
import AddEntry from "../components/add-entry";

class Home extends Component {
  state = {
    entries: {},
    isLoaded: false
  };

  render() {
    return (
      <React.Fragment>
        {this.renderAddEntry()}
        <div id="posts-container">
          <Entries
            auth={this.props.auth}
            entries={this.state.entries}
            isLoaded={this.state.isLoaded}
            onDelete={this.handleDelete}
          />
        </div>
      </React.Fragment>
    );
  }

  renderAddEntry() {
    if (this.props.auth.isAuthenticated()) {
      return <AddEntry onSubmit={this.handleSubmit} />;
    } else return null;
  }

  addEntry = (text, accessToken) => {
    fetch("/api/entries", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${accessToken}`
      },
      body: JSON.stringify({
        text: text
      })
    }).then(response => {
      if (response.ok) {
        this.updateEntriesList();
      }
    });
  };

  // Add
  handleSubmit = text => {
    const accessToken = this.props.auth.getAccessToken();

    if (accessToken !== null) {
      var result = this.props.auth.tryRefreshToken(() => {
        this.addEntry(text, accessToken);
      });

      if (result === false) {
        this.addEntry(text, accessToken);
      }
    }
  };

  deleteEntry = (keyId, accessToken) => {
    fetch("/api/entries/" + keyId, {
      method: "DELETE",
      headers: {
        Accept: "application/json",
        Authorization: `Bearer ${accessToken}`
      }
    }).then(response => {
      if (response.ok) {
        this.updateEntriesList();
      }
    });
  };

  // Delete
  handleDelete = keyId => {
    const accessToken = this.props.auth.getAccessToken();

    if (accessToken !== null) {
      var result = this.props.auth.tryRefreshToken(() => {
        this.deleteEntry(keyId, accessToken);
      });

      if (result === false) {
        this.deleteEntry(keyId, accessToken);
      }
    }
  };

  // Get
  updateEntriesList = () => {
    this.isLoaded = false;

    fetch("/api/entries", {
      headers: {
        Accept: "application/json"
      }
    }).then(response => {
      if (response.ok) {
        response.json().then(json => {
          this.setState({
            isLoaded: true,
            entries: json
          });
        });
      }
    });
  };

  componentDidMount() {
    this.updateEntriesList();
  }
}

export default Home;
