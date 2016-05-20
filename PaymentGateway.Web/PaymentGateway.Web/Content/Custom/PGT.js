var PGT =
{
    initilize: function() {
        var array = ["/Content/Images/M2.jpg", "/Content/Images/M3.jpg", "/Content/Images/M4.jpg"];

        for (var i = 0; i < array.length; i++) {
            $('#imgContent').append("<img src=" + array[i] + " alt='mainImage' class='subImg'>");

        }
    }



};


