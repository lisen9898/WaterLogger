function calculate() {
    let tbl = document.querySelector("#records");
    let resultArea = document.querySelector("#result");

    let result = 0;

    for (let i = 1; i < tbl.rows.length; i++) {
        let row = tbl.rows[i];
        let value = row.cells[1].querySelector(".quantity");
        value = parseFloat(value.innerHTML);

        let unit = row.cells[1].querySelector(".unit").innerHTML.trim();
        console.log(unit)

        switch (unit) {
            case "L":
                value *= 1000;
                break;
            case "mm³":
                value *= 0.001;
                break;
            case "fl.oz":
                value *= 29.5735; // U.S. fluid ounce to ml
                break;
            case "pts":
                value *= 473.176; // U.S. pint to ml
                break;
            case "cup":
                value *= 236.588; // U.S. cup to ml
                break;
            case "qts":
                value *= 946.353; // U.S. quart to ml
                break;
            case "gal":
                value *= 3785.41; // U.S. gallon to ml
                break;
            case "tbsp":
                value *= 14.7868; // U.S. tablespoon to ml
                break;
            case "tsp":
                value *= 4.92892; // U.S. teaspoon to ml
                break;
            case "ifloz":
                value *= 28.4131; // Imperial fluid ounce to ml
                break;
            case "ipts":
                value *= 568.261; // Imperial pint to ml
                break;
            case "icup":
                value *= 284.131; // Imperial cup to ml
                break;
            case "iqts":
                value *= 1136.52; // Imperial quart to ml
                break;
            case "igal":
                value *= 4546.09; // Imperial gallon to ml
                break;
            case "itbsp":
                value *= 17.7582; // Imperial tablespoon to ml
                break;
            case "itsp":
                value *= 5.91939; // Imperial teaspoon to ml
                break;
        }
        result += value;
    }
    resultArea.innerHTML = result.toFixed(2) + ' mL';
}