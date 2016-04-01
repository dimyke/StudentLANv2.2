$('#popUp_AreUSure').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var id = button.data('orderid')
    var name = button.data('ordername')    
    // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
    // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
    var modal = $(this)
    console.log(modal.find('.orderid-hier'));
    modal.find('.orderid-hier').attr('onclick', 'location.href=\'/KitchenAdmin/ToggleDelete?orderid=' + id + '\'')
    modal.find('.modal-body').text('Ben je zeker dat je orderid: ' + id + ' van user: ' + name + ' wilt verwijderen?')


})
//.onclick('location.href = \'@Url.Action("ToggleDelete", "KitchenAdmin", new { orderid =' + id + '})\'')