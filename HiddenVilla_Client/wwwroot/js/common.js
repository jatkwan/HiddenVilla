window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, 'Operation Succed');
    }
    if (type === "error") {
        toastr.error(message, 'Operation Failed');
    }
}
