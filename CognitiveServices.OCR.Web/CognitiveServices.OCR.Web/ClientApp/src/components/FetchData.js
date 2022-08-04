import React, { Component } from 'react';
import originalImage from './../test.png';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderForecastsTable(forecasts) {
    return (
      <div className='row'>
        <div className='col-md-6'>
          <img src={originalImage} />
        </div>
        <div className='col-md-6'>
          <table className='table table-striped' aria-labelledby="tabelLabel">
            <tbody>
              {forecasts.recognizedLines.map(line =>
                <tr>
                  <td>{line}</td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch('textrecognation');
    const data = await response.json();
    this.setState({ forecasts: data, loading: false });
  }
}
