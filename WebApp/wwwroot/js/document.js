$(document).ready(function () {
    var listDocumentId = [];
    $("input:checkbox").each(function () {
        var $this = $(this);
        if ($this.is(":checked")) {
            listDocumentId.push($this.attr("id"))
        }
    });
    $("#compare").click(function () {
        alert(listDocumentId[1]);
    });
});