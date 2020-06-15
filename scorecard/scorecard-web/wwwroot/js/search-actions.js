/**
 * Js Actions for Search page
 * */

var fields;
var search;

/**
 * Post-loading actions
 * */
$(document).ready(function (event) {
    searchFields();
});

/**
 * Retrieves the list of searchable fields
 * */
function searchFields() {
    $("#blockui").show();
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            fields = JSON.parse(this.responseText);
            fields = $.map(fields.scorecard.mappings, function (val, i) {
                return val.full_name;
            });
            $("#blockui").hide();
        }
    };
    xhttp.open("GET", "http://localhost:9200/scorecard/_mapping/field/*", true);
    xhttp.send();
}

/**
 * Filters the available fields with data from given input and shows the autocomplete list for key selection
 * @param {any} input
 */
function filterFields(e, input) {
    if (e.keyCode == 27) {
        hideSearch();
    }
    else {
        var text = input.value;

        var filtered = $.grep(fields, function (n) {
            return n.toLowerCase().includes(text.toLowerCase());
        });

        $("#results ul").html("");

        var patt = new RegExp(text, "i");

        $.each(filtered, function (i, val) {
            var m = val.match(patt);
            var v = val.replace(m, "<b>" + m + "</b>");
            $("#results ul").append("<li>" + v + "</li>");
        });

        $("#results").insertAfter(input).show();

        $("#results li").bind("click", function () {
            selectKey(input, $(this).text());
        });
    }
}

/**
 * Selects a value for given input
 * @param {any} input
 * @param {any} value
 */
function selectKey(input, value) {
    input.value = value;
    $("#results ul").html("");
    $("#results").hide();
}

/**
 * Creates a row in the page to add a new term on the search 
 * */
function addTerm() {
    hideSearch();
    var newElem = $(".term:last").clone();
    $(newElem).find("input").val("");
    $(newElem).insertAfter($(".term:last"));
}

/**
 * Removes given term. Clears the row, if there is only one term on the page.
 * @param {any} element
 */
function removeTerm(element) {
    if ($(".term").length > 1) {
        hideSearch();
        $(element).closest(".term").remove();
    }
    else {
        $(".term input").val("");
    }
}

/**
 * Hides the autocomplete list and moves it to a safe location, preventing being lost or duplicated.
 * */
function hideSearch() {
    $("#results").hide().insertBefore($(".term:first"));
}

/**
 * Searches on the elasticsearch server, using the terms filled by the user.
 * Mounts the string query in comma-separated style for each key:value term.
 * Executes a search only if at least one key:value term is filled correctly.
 * */
function search() {
    var queryTerms = [];

    $(".term").each(function () {
        var key = $(this).find(".field").val();
        var value = $(this).find(".value").val();
        if (key !== "" && value !== "") {
            queryTerms.push(key + ":" + value);
        }
    });

    if (queryTerms.length > 0) {
        var operator = $("input[name=optradio]:checked").val();
        document.location = "/search/" + operator + "/" + queryTerms.join(",");
    }
    else {
        alert("Busca não pode ser vazia.");
    }
}

/**
 * Toggles the view of some result in the result list
 * @param {any} element
 */
function toggleView(element) {
    if ($(element).hasClass("fa-chevron-up")) {
        $(element).closest(".result-item").find("table").hide();
        $(element).removeClass("fa-chevron-up");
        $(element).addClass("fa-chevron-down");
    }
    else {
        $(element).closest(".result-item").find("table").show();
        $(element).addClass("fa-chevron-up");
        $(element).removeClass("fa-chevron-down");
    }
}

/**
 * Prevents hide the search list before its click event is triggered
 * */
function DebounceHideSearch() {
    setTimeout(function () { hideSearch(); }, 100);
}