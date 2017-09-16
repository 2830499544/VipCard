var provinceID;
var CityID;
var CountyID;
var VillageID;
$(document).ready(function () {
    $("#ucSysArea_sltCity").bind("change", { foo: "ucSysArea" }, City);
    $("#ucSysArea_sltCounty").bind("change", { foo: "ucSysArea" }, County);
    $("#ucSysArea_sltProvince").bind("change", { foo: "ucSysArea" }, Province);
    $("#ucSysArea_sltVillage").bind("change", { foo: "ucSysArea" }, Village);
});

function GetNextName(pid, controlID) {
    doAjax("../",
        "GetNextName",
        { "pid": pid },
        "json",
        function (json) {
            if (json != "") {
                for (var i = 0; i < json.length; i++) {
                    $("#" + controlID).append("<option value='" + json[i].ID + "'>" + json[i].Name + "</option>");
                }
            }
        },
        false
       );
}
function Province(event) {
    var obj = event.data.foo;
    $("#" + obj + "_sltCity").empty();
    $("#" + obj + "_sltCounty").empty();
    $("#" + obj + "_sltVillage").empty();
    $("#" + obj + "_sltCity").append("<option value=''>=== 请选择 ===</option>");
    $("#" + obj + "_sltCounty").append("<option value=''>=== 请选择 ===</option>");
    $("#" + obj + "_sltVillage").append("<option value=''>=== 请选择 ===</option>");
    provinceID = $("#" + obj + "_sltProvince").val();
    GetNextName(provinceID, "" + obj + "_sltCity");
}
function City(event) {
    var obj = event.data.foo;
    $("#" + obj + "_sltCounty").empty();
    $("#" + obj + "_sltVillage").empty();
    
    $("#" + obj + "_sltCounty").append("<option value=''>=== 请选择 ===</option>");
    $("#" + obj + "_sltVillage").append("<option value=''>=== 请选择 ===</option>");
    CityID = $("#" + obj + "_sltCity").val();
    GetNextName(CityID, "" + obj + "_sltCounty");
}
function County(event) {
    var obj = event.data.foo;
    $("#" + obj + "_sltVillage").empty();
    $("#" + obj + "_sltVillage").append("<option value=''>=== 请选择 ===</option>");
    CountyID = $("#" + obj + "_sltCounty").val();
    GetNextName(CountyID, "" + obj + "_sltVillage");
}
function Village(event) {
    var obj = event.data.foo;
    VillageID = $("#" + obj + "_sltVillage").val();
}
function Empty() {
    $("#" + obj + "_sltProvince").val("");
    $("#" + obj + "_sltCity").val("");
    $("#" + obj + "_sltCounty").val("");
    $("#" + obj + "_sltVillage").val("");

}
