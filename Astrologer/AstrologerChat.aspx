<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AstrologerChat.aspx.cs" Inherits="AstroApp.Astrologer.AstrologerChat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="mobile-web-app-capable" content="yes" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, viewport-fit=cover" />
    <title>Astrologer Chat</title>
    <link rel="stylesheet" href="../style.css" />
    <script src="../Scripts/jquery-3.7.1.min.js"></script>
    <script src="../Scripts/jquery.signalR-2.4.3.min.js"></script>
    <script src="../signalr/hubs"></script>
    <style>
        #chat-wrapper {
            scroll-behavior: smooth;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <asp:HiddenField ID="hdnUserID" runat="server" />
        <asp:HiddenField ID="hdnAstrologerID" runat="server" />

        <div class="header-area" id="headerArea">
            <div class="container">
                <div class="header-content position-relative d-flex align-items-center justify-content-between">
                    <div class="chat-user--info d-flex align-items-center">
                        <div class="back-button">
                            <a href="AstroHome.aspx">
                                <i class="bi bi-arrow-left-short"></i>
                            </a>
                        </div>
                        <div class="user-thumbnail-name">
                            <asp:Image ID="ImgUser" runat="server" />
                            <div class="info ms-1">
                                <p>
                                    <asp:Literal ID="lblUsername" runat="server"></asp:Literal>
                                </p>
                                <span class="<%= GetOnlineStatus() %>">
                                    <asp:Literal ID="lblOnlineStatus" runat="server"></asp:Literal></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="page-content-wrapper py-3" id="chat-wrapper" style="height: 400px; overflow-y: auto;">
            <div class="container">

                <div style="position: fixed; right: 20px; bottom: 100px; z-index: 999">
                    <div id="myToast" class="toast custom-toast-1 toast-danger mb-2 fade" role="alert" aria-live="assertive" aria-atomic="true" data-bs-delay="5000" data-bs-autohide="true">
                        <div class="toast-body">
                            <i class="bi bi-exclamation-diamond text-white h1 mb-0"></i>
                            <div class="toast-text ms-3 me-2">
                                <p class="mb-0 text-white">File size should not exceed 5MB</p>
                            </div>
                        </div>
                        <button class="btn btn-close btn-close-white position-absolute p-1" type="button" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                    <div id="myAudioToast" class="toast custom-toast-1 toast-danger mb-2 fade" role="alert" aria-live="assertive" aria-atomic="true" data-bs-delay="5000" data-bs-autohide="true">
                        <div class="toast-body">
                            <i class="bi bi-exclamation-diamond text-white h1 mb-0"></i>
                            <div class="toast-text ms-3 me-2">
                                <p class="mb-0 text-white">Unable to access microphone. Please check permissions.</p>
                            </div>
                        </div>
                        <button class="btn btn-close btn-close-white position-absolute p-1" type="button" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                </div>

                <div class="chat-content-wrap" id="chat-content">
                    <!-- Chat messages will be appended dynamically -->
                    <asp:Repeater ID="rptChat" runat="server">
                        <ItemTemplate>
                            <div class="single-chat-item <%# Convert.ToInt32(Eval("sender_id")) ==Convert.ToInt32(hdnAstrologerID.Value) ? "outgoing" : "incoming" %> <%# GetReadUnreadClass(Container.DataItem) %>" data-id="<%# Eval("message_id") %>">
                                <div class="user-avatar mt-1">
                                    <span class="name-first-letter">A</span>
                                    <img src="<%# GetUserImage(Eval("sender_id")) %>" alt="" />
                                </div>
                                <div class="user-message">
                                    <div class="message-content">
                                        <div class="single-message">
                                            <p runat="server" id="pMessage" visible='<%# Eval("message_type").ToString() == "text" %>'>
                                                <%# Eval("message_text") %>
                                            </p>
                                            <div class="gallery-img position-relative" id="divMedia" runat="server" visible='<%# Eval("message_type").ToString() == "media" %>'>
                                                <%# GetMediaHtml(Eval("message_text").ToString()) %>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="message-time-status">
                                        <div class="sent-time"><%# Eval("created_at", "{0:hh:mm tt}") %></div>
                                        <div class="sent-status <%# Convert.ToBoolean(Eval("is_read")) ? "seen" : "" %> <%# Convert.ToInt32(Eval("sender_id")) ==Convert.ToInt32(hdnAstrologerID.Value) ? "" : "d-none" %>">
                                            <i class="bi bi-check"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="chat-footer">
            <div class="container h-100">
                <div class="chat-footer-content h-100 d-flex align-items-center">
                    <div class="chat-form">

                        <input type="file" id="fileInput" style="display: none;" accept=".jpg,.jpeg,.png,.gif,.pdf,.doc,.docx" />
                        <button class="btn btn-attachment" type="button" onclick="$('#fileInput').click()">
                            <i class="bi bi-paperclip"></i>
                        </button>
                        <button class="btn btn-voice" id="btnVoice" type="button">
                            <i class="bi bi-mic"></i>
                        </button>
                        <input class="form-control" id="txtMessage" type="text" placeholder="Type here..." />

                        <button class="btn btn-submit ms-2" id="btnSend" type="button">
                            <i class="bi bi-cursor"></i>
                        </button>
                    </div>
                </div>
            </div>
            <div id="recordingIndicator" class="recording-indicator" style="display: none;">
                <span class="recording-time">00:00</span>
                <button class="btn btn-stop-recording" type="button" id="btnStopRecording">
                    <i class="bi bi-stop-circle"></i>
                </button>
                <button class="btn btn-cancel-recording" type="button" id="btnCancelRecording">
                    <i class="bi bi-x-circle"></i>
                </button>
            </div>
        </div>
    </form>
    <script>
        $(function () {
            let indianTime = new Date().toLocaleTimeString("en-US", {
                timeZone: 'Asia/Kolkata'
            });

            $(document).ready(function () {
                // Scroll to the latest message when the chat page loads
                scrollToLatestMessage();
            });

            // Function to scroll the chat container to the latest message
            function scrollToLatestMessage() {
                var chatContent = $("#chat-wrapper"); // Replace #chat-wrapper with your chat container's ID or class
                chatContent.scrollTop(chatContent[0].scrollHeight);
            }

            var chat = $.connection.chatHub;

            // Receive message
            chat.client.receiveMessage = function (messageId, senderId, message, messageType) {
                var messageHtml = '';
                var currentUserId = <%= hdnAstrologerID.Value %>;

                // Check if the sender is the current user
                if (senderId == currentUserId) {
                    // Sent message template
                    messageHtml = "";
                    if (messageType === "text") {
                        // Render text message
                        messageHtml = `
<div class="single-chat-item outgoing">
    <div class="user-avatar mt-1">
        <span class="name-first-letter">A</span>
        <img src="<%= astrologerImage %>" alt="">
    </div>
    <div class="user-message">
        <div class="message-content">
            <div class="single-message">
                <p>${message}</p>
            </div>
        </div>
        <div class="message-time-status">
            <div class="sent-time">${indianTime}</div>
            <div class="sent-status" id="message-${messageId}">
                <i class="bi bi-check"></i>
            </div>
        </div>
    </div>
</div>`;
                    } else if (messageType === "media") {
                        // Render media message
                        const fileExtension = message.split('.').pop().toLowerCase();
                        let mediaContent = "";

                        if (["jpg", "jpeg", "png", "gif"].includes(fileExtension)) {
                            // Image file
                            mediaContent = `
    <div class="gallery-img position-relative">
        <a class="venobox portfolio-item large single-gallery-item image-zooming-in-out" title="${message}"
            data-gall="gallery01" href="../Uploads/Chats/${message}">
            <img src="../Uploads/Chats/${message}" alt="">
                            <div class='fav-icon shadow'>
           <a id='downloadLink' class='bi bi-file-earmark-arrow-down-fill' href='../Uploads/Chats/${message}' download>
                </a>
</div>
        </a>
    </div>`;
                        } else if (fileExtension === "pdf") {
                            // PDF file
                            mediaContent = `
    <a href="../Uploads/Chats/${message}" class="btn btn-danger" download>
        <i class="bi bi-file-earmark-pdf-fill"></i> ${message}
    </a>`;
                        } else if (["doc", "docx"].includes(fileExtension)) {
                            // Word document
                            mediaContent = `
    <a href="../Uploads/Chats/${message}" class="btn btn-info" download>
        <i class="bi bi-file-earmark-word-fill"></i> ${message}
    </a>`;
                        }
                        else if (fileExtension === "wav") {
                            // Audio file
                            mediaContent = `
                    <audio controls>
                        <source src="../Uploads/VoiceNotes/${message}" type='audio/wav'>
                        Your browser does not support the audio element.
                    </audio>
<br/>
<a href="../Uploads/VoiceNotes/${message}" download=${message}>
 <i class='bi bi-file-earmark-arrow-down-fill'></i>
</a>`;
                        }
                        else {
                            // Other file types
                            mediaContent = `
    <a href="../Uploads/Chats/${message}" class="btn btn-secondary" download>
        <i class="bi bi-file-earmark"></i> ${message}
    </a>`;
                        }

                        messageHtml = `
<div class="single-chat-item outgoing">
    <div class="user-avatar mt-1">
        <span class="name-first-letter">A</span>
        <img src="<%= astrologerImage %>" alt="">
    </div>
    <div class="user-message">
    <div class="message-content">
        <div class="single-message">
${mediaContent}
           </div>
            </div>
        <div class="message-time-status">
            <div class="sent-time">${indianTime}</div>
            <div class="sent-status" id="message-${messageId}">
                <i class="bi bi-check"></i>
            </div>
        </div>
    </div>
</div>`;
                    }
                } else {
                    // Received message template
                    messageHtml = "";
                    if (messageType === "text") {
                        // Render text message
                        messageHtml = `
<div class="single-chat-item incoming" data-id="${messageId}">
    <div class="user-avatar mt-1">
        <span class="name-first-letter">A</span>
        <img src="<%= userImage %>" alt="">
    </div>
    <div class="user-message">
        <div class="message-content">
            <div class="single-message">
                <p>${message}</p>
            </div>
        </div>
        <div class="message-time-status">
            <div class="sent-time">${indianTime}</div>
           <div class="sent-status seen d-none">
                <i class="bi bi-check"></i>
            </div>
        </div>
    </div>
</div>`;
                    } else if (messageType === "media") {
                        // Render media message
                        const fileExtension = message.split('.').pop().toLowerCase();
                        let mediaContent = "";

                        if (["jpg", "jpeg", "png", "gif"].includes(fileExtension)) {
                            // Image file
                            mediaContent = `
    <div class="gallery-img position-relative">
        <a class="venobox portfolio-item large single-gallery-item image-zooming-in-out" title="${message}"
            data-gall="gallery01" href="../Uploads/Chats/${message}">
            <img src="../Uploads/Chats/${message}" alt="">
                                        <div class='fav-icon shadow'>
           <a id='downloadLink' class='bi bi-file-earmark-arrow-down-fill' href='../Uploads/Chats/${message}' download>
                </a>
</div>
        </a>
    </div>`;
                        } else if (fileExtension === "pdf") {
                            // PDF file
                            mediaContent = `
    <a href="../Uploads/Chats/${message}" class="btn btn-danger" download>
        <i class="bi bi-file-earmark-pdf-fill"></i> ${message}
    </a>`;
                        } else if (["doc", "docx"].includes(fileExtension)) {
                            // Word document
                            mediaContent = `
    <a href="../Uploads/Chats/${message}" class="btn btn-info" download>
        <i class="bi bi-file-earmark-word-fill"></i> ${message}
    </a>`;
                        }
                        else if (fileExtension === "wav") {
                            // Audio file
                            mediaContent = `
                    <audio controls>
                        <source src="../Uploads/VoiceNotes/${message}" type='audio/wav'>
                        Your browser does not support the audio element.
                    </audio>
<br/>
<a href="../Uploads/VoiceNotes/${message}" download=${message}>
 <i class='bi bi-file-earmark-arrow-down-fill'></i>
</a>`;
                        }
                        else {
                            // Other file types
                            mediaContent = `
    <a href="../Uploads/Chats/${message}" class="btn btn-secondary" download>
        <i class="bi bi-file-earmark"></i> ${message}
    </a>`;
                        }

                        messageHtml = `
<div class="single-chat-item incoming" data-id="${messageId}">
    <div class="user-avatar mt-1">
        <span class="name-first-letter">A</span>
        <img src="<%= userImage %>" alt="">
    </div>
    <div class="user-message">
    <div class="message-content">
        <div class="single-message">
${mediaContent}
           </div>
            </div>
        <div class="message-time-status">
            <div class="sent-time">${indianTime}</div>
             <div class="sent-status seen d-none">
                <i class="bi bi-check"></i>
            </div>
        </div>
    </div>
</div>`;
                    }
                }
                $("#chat-content").append(messageHtml);

                $('.venobox').venobox();

                var chatContent = $("#chat-wrapper");
                chatContent.scrollTop(chatContent[0].scrollHeight);

                checkUnreadMessagesInViewport();
            };


            $(function () {
                var toastAudioElement = document.getElementById('myAudioToast');
                var toastAudio = new bootstrap.Toast(toastAudioElement);
                toastAudio.hide();

                let mediaRecorder;
                let audioChunks = [];
                let recordingTimer;
                let recordingDuration = 0;
                const MAX_RECORDING_TIME = 300; // 5 minutes in seconds

                // Voice recording functions
                async function startRecording() {
                    try {
                        const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
                        mediaRecorder = new MediaRecorder(stream);
                        audioChunks = [];

                        mediaRecorder.ondataavailable = (event) => {
                            audioChunks.push(event.data);
                        };

                        mediaRecorder.onstop = () => {
                            const audioBlob = new Blob(audioChunks, { type: 'audio/wav' });
                            sendVoiceMessage(audioBlob);
                            stopRecordingTimer();
                        };

                        mediaRecorder.start();
                        startRecordingTimer();

                        $('#btnVoice').addClass('recording');
                        $('#recordingIndicator').show();
                        $('#txtMessage').prop('disabled', true);
                        $('#btnSend').prop('disabled', true);
                        $('#fileInput').prop('disabled', true);
                    } catch (error) {
                        console.error('Error accessing microphone:', error);
                        toastAudio.show();
                        //alert('Unable to access microphone. Please check permissions.');
                    }
                }

                function stopRecording() {
                    if (mediaRecorder && mediaRecorder.state === 'recording') {
                        mediaRecorder.stop();
                        mediaRecorder.stream.getTracks().forEach(track => track.stop());
                        resetRecordingUI();
                    }
                }

                function cancelRecording() {
                    if (mediaRecorder && mediaRecorder.state === 'recording') {
                        mediaRecorder.stream.getTracks().forEach(track => track.stop());
                        resetRecordingUI();
                    }
                }

                function resetRecordingUI() {
                    $('#btnVoice').removeClass('recording');
                    $('#recordingIndicator').hide();
                    $('#txtMessage').prop('disabled', false);
                    $('#btnSend').prop('disabled', false);
                    $('#fileInput').prop('disabled', false);
                    stopRecordingTimer();
                    recordingDuration = 0;
                    updateRecordingTime();
                }

                function startRecordingTimer() {
                    recordingDuration = 0;
                    updateRecordingTime();
                    recordingTimer = setInterval(() => {
                        recordingDuration++;
                        updateRecordingTime();
                        if (recordingDuration >= MAX_RECORDING_TIME) {
                            stopRecording();
                        }
                    }, 1000);
                }

                function stopRecordingTimer() {
                    if (recordingTimer) {
                        clearInterval(recordingTimer);
                        recordingTimer = null;
                    }
                }

                function updateRecordingTime() {
                    const minutes = Math.floor(recordingDuration / 60).toString().padStart(2, '0');
                    const seconds = (recordingDuration % 60).toString().padStart(2, '0');
                    $('.recording-time').text(`${minutes}:${seconds}`);
                }

                async function sendVoiceMessage(audioBlob) {
                    const reader = new FileReader();
                    reader.onload = function (e) {
                        var senderId = $("#hdnAstrologerID").val();
                        var receiverId = $("#hdnUserID").val();
                        var isAstrologer = true;

                        // Send the audio data to the server
                        chat.server.sendMediaMessage(
                            senderId,
                            receiverId,
                            e.target.result,
                            `voice_${new Date().getTime()}.wav`,
                            'audio/wav',
                            isAstrologer
                        );
                    };
                    reader.readAsDataURL(audioBlob);
                }

                // Add event listeners
                $('#btnVoice').on('click', startRecording);
                $('#btnStopRecording').on('click', stopRecording);
                $('#btnCancelRecording').on('click', cancelRecording);
            });



            // Function to mark messages as read
            function markMessagesAsRead(messageIds) {
                if (messageIds.length > 0) {
                    chat.server.markMessageAsRead(messageIds);
                }
            }

            // Function to check unread messages in the viewport
            function checkUnreadMessagesInViewport() {
                var wrapper = $('#chat-wrapper');
                var unreadMessages = [];

                // Find all unread messages visible in the viewport
                $('.single-chat-item.incoming').each(function () {
                    var $message = $(this);
                    var messageId = $message.data('id');

                    // Check if the message is visible in the chat viewport
                    if ($message.offset().top >= wrapper.offset().top &&
                        $message.offset().top < wrapper.offset().top + wrapper.height()) {
                        unreadMessages.push(messageId);

                        // Mark the message as read in the UI
                        $message.removeClass('incoming').addClass('read');

                        //chat.server.markMessageAsRead(unreadMessages);
                    }
                });

                // Send unread message IDs to the backend
                markMessagesAsRead(unreadMessages);
            }

            // Notify the sender when read receipts are updated
            chat.client.notifyReadReceipt = function (messageIds) {
                messageIds.forEach(function (messageId) {
                    $(`#message-${messageId}`).addClass("seen");
                });
            };

            var toastElement = document.getElementById('myToast');
            var toast = new bootstrap.Toast(toastElement);
            toast.hide();

            // Start SignalR connection
            $.connection.hub.start().done(function () {
                console.log("SignalR connection established.");
                // Register the connection
                chat.server.registerConnection($("#hdnUserID").val(), $("#hdnAstrologerID").val(), true);// Pass `true` if astrologer

                var initialUnreadMessages = [];
                $('.single-chat-item.incoming').each(function () {
                    initialUnreadMessages.push($(this).data('id'));
                });

                // Send all unread messages to the backend
                markMessagesAsRead(initialUnreadMessages);

                // Attach a scroll event listener to check for unread messages in the viewport
                $('#chat-wrapper').on('scroll', function () {
                    checkUnreadMessagesInViewport();
                });

                // Send message on button click
                $("#btnSend").click(function () {
                    var senderId = $("#hdnAstrologerID").val();
                    var receiverId = $("#hdnUserID").val();
                    var message = $("#txtMessage").val();

                    if (message.trim() === "") return;

                    // Send message to server
                    chat.server.sendMessage(senderId, receiverId, message, true); // `false` indicates the sender is a user
                    //chat.client.receiveMessage(messageId,senderId, message);
                    $("#txtMessage").val("");
                });

                $('#fileInput').on('change', function (e) {
                    var file = e.target.files[0];
                    if (!file) return;

                    // Check file size
                    if (file.size > 5 * 1024 * 1024) { // 5MB
                        //alert('File size should not exceed 5MB');
                        toast.show();
                        return;
                    }

                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var senderId = $("#hdnAstrologerID").val();
                        var receiverId = $("#hdnUserID").val();
                        var isAstrologer = true;

                        // Send the file data to the server
                        chat.server.sendMediaMessage(
                            senderId,
                            receiverId,
                            e.target.result,
                            file.name,
                            file.type,
                            isAstrologer
                        );
                    };
                    reader.readAsDataURL(file);

                    var chatContent = $("#chat-wrapper");
                    chatContent.scrollTop(chatContent[0].scrollHeight);

                    checkUnreadMessagesInViewport();
                });
            });


        });
</script>
    <script src="../js/bootstrap.bundle.min.js"></script>
    <script src="../js/slideToggle.min.js"></script>
    <script src="../js/internet-status.js"></script>
    <script src="../js/tiny-slider.js"></script>
    <script src="../js/venobox.min.js"></script>
    <script src="../js/countdown.js"></script>
    <script src="../js/rangeslider.min.js"></script>
    <script src="../js/vanilla-dataTables.min.js"></script>
    <script src="../js/index.js"></script>
    <script src="../js/imagesloaded.pkgd.min.js"></script>
    <script src="../js/isotope.pkgd.min.js"></script>
    <script src="../js/dark-rtl.js"></script>
    <script src="../js/active.js"></script>
    <script src="../js/pwa.js"></script>
    <script src="../js/apexcharts.min.js"></script>
    <script src="../js/chart-active.js"></script>
</body>
</html>
