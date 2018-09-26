import React, { Component } from "react";

import Entry from "./entry";

class Entries extends Component {
  getEnteries() {
    if (this.props.entries.length > 0) {
      return this.props.entries.map(entry => (
        <Entry
          key={entry.id}
          auth={this.props.auth}
          author={entry.author}
          date={entry.createTime}
          dataKey={entry.id}
          onDelete={this.props.onDelete}
        >
          {entry.text}
        </Entry>
      ));
    } else {
      return <h3>Nic tutaj nie ma ;(</h3>;
    }
  }

  render() {
    if (this.props.isLoaded) return this.getEnteries();
    else return <h3>Ładowanie...</h3>;
  }
}

export default Entries;
