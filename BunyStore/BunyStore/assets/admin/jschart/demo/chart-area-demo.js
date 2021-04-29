// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';



function number_format(number, decimals, dec_point, thousands_sep) {
  // *     example: number_format(1234.56, 2, ',', ' ');
  // *     return: '1 234,56'
  number = (number + '').replace(',', '').replace(' ', '');
  var n = !isFinite(+number) ? 0 : +number,
    prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
    sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
    dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
    s = '',
    toFixedFix = function(n, prec) {
      var k = Math.pow(10, prec);
      return '' + Math.round(n * k) / k;
    };
  // Fix for IE parseFloat(0.55).toFixed(0) = 0;
  s = (prec ? toFixedFix(n, prec) : '' + Math.round(n)).split('.');
  if (s[0].length > 3) {
    s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
  }
  if ((s[1] || '').length < prec) {
    s[1] = s[1] || '';
    s[1] += new Array(prec - s[1].length + 1).join('0');
  }
  return s.join(dec);
}

//Khai bao gia tri cac thang
var TH1 = $("#TH1").text(); //tháng 1 
var TH2 = $("#TH2").text(); //tháng 2
var TH3 = $("#TH3").text(); //tháng 3
var TH4 = $("#TH4").text(); //tháng 4
var TH5 = $("#TH5").text(); //tháng 5
var TH6 = $("#TH6").text(); //tháng 6
var TH7 = $("#TH7").text(); //tháng 7
var TH8 = $("#TH8").text(); //tháng 8
var TH9 = $("#TH9").text(); //tháng 9
var TH10 = $("#TH10").text(); //tháng 10
var TH11 = $("#TH11").text(); //tháng 11
var TH12 = $("#TH12").text(); //tháng 12

$("#TH1").text(number_format(TH1) + " VND")
$("#TH2").text(number_format(TH2) + " VND")
$("#TH3").text(number_format(TH3) + " VND")
$("#TH4").text(number_format(TH4) + " VND")
$("#TH5").text(number_format(TH5) + " VND")
$("#TH6").text(number_format(TH6) + " VND")
$("#TH7").text(number_format(TH7) + " VND")
$("#TH8").text(number_format(TH8) + " VND")
$("#TH9").text(number_format(TH9) + " VND")
$("#TH10").text(number_format(TH10) + " VND")
$("#TH11").text(number_format(TH11) + " VND")
$("#TH12").text(number_format(TH12) + " VND")
// Area Chart Example
var ctx = document.getElementById("myAreaChart");
var myLineChart = new Chart(ctx, {
  type: 'line',
  data: {
    labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
    datasets: [{
        label: "Revenue", /*doanh thu*/
      lineTension: 0.3,
      backgroundColor: "rgba(78, 115, 223, 0.05)",
      borderColor: "rgba(78, 115, 223, 1)",
      pointRadius: 3,
      pointBackgroundColor: "rgba(78, 115, 223, 1)",
      pointBorderColor: "rgba(78, 115, 223, 1)",
      pointHoverRadius: 3,
      pointHoverBackgroundColor: "rgba(78, 115, 223, 1)",
      pointHoverBorderColor: "rgba(78, 115, 223, 1)",
      pointHitRadius: 10,
      pointBorderWidth: 2,
      data: [TH1, TH2, TH3, TH4, TH5, TH6, TH7, TH8, TH9, TH10, TH11, TH12],
    }],
  },
  options: {
    maintainAspectRatio: false,
    layout: {
      padding: {
        left: 10,
        right: 25,
        top: 25,
        bottom: 0
      }
    },
    scales: {
      xAxes: [{
        time: {
          unit: 'date'
        },
        gridLines: {
          display: false,
          drawBorder: false
        },
        ticks: {
          maxTicksLimit: 7
        }
      }],
      yAxes: [{
        ticks: {
          maxTicksLimit: 5,
          padding: 10,
          // VND
          callback: function(value, index, values) {
            return number_format(value)+" VND";
          }
        },
        gridLines: {
          color: "rgb(234, 236, 244)",
          zeroLineColor: "rgb(234, 236, 244)",
          drawBorder: false,
          borderDash: [2],
          zeroLineBorderDash: [2]
        }
      }],
    },
    legend: {
      display: false
    },
    tooltips: {
      backgroundColor: "rgb(255,255,255)",
      bodyFontColor: "#858796",
      titleMarginBottom: 10,
      titleFontColor: '#6e707e',
      titleFontSize: 14,
      borderColor: '#dddfeb',
      borderWidth: 1,
      xPadding: 15,
      yPadding: 15,
      displayColors: false,
      intersect: false,
      mode: 'index',
      caretPadding: 10,
      callbacks: {
        label: function(tooltipItem, chart) {
          var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
          return datasetLabel + ': ' + number_format(tooltipItem.yLabel)+" VND";
        }
      }
    }
  }
});
