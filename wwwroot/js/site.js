// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#success").click(function () {
    $(".notify").toggleClass("active");
    $("#notifyType").toggleClass("success");
    
    setTimeout(function(){
      $(".notify").removeClass("active");
      $("#notifyType").removeClass("success");
    },2000);
  });
  
  $("#failure").click(function () {
    $(".notify").addClass("active");
    $("#notifyType").addClass("failure");
    
    setTimeout(function(){
      $(".notify").removeClass("active");
      $("#notifyType").removeClass("failure");
    },2000);
  });